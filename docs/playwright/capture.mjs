import { chromium } from '@playwright/test';
import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const SHOTS_DIR = path.resolve(__dirname, '..', 'screenshots');
const BASE = 'http://localhost/ams';
const USER = 'admin';
const PASS = 'super';

if (!fs.existsSync(SHOTS_DIR)) fs.mkdirSync(SHOTS_DIR, { recursive: true });

async function shoot(page, name, opts = {}) {
    const file = path.join(SHOTS_DIR, `${name}.png`);
    await page.screenshot({ path: file, fullPage: false, ...opts });
    console.log(`  wrote ${name}.png`);
}

async function login(page) {
    console.log(`> login`);
    await page.goto(`${BASE}/Login.aspx`, { waitUntil: 'networkidle' });
    await shoot(page, 'login');
    await page.fill('input[id$="dviUserName_Edit_I"]', USER);
    await page.fill('input[id$="dviPassword_Edit_I"]', PASS);
    await page.locator('#Logon_PopupActions_Menu').getByText('Log On', { exact: true }).first().click();
    await page.waitForLoadState('networkidle', { timeout: 30000 }).catch(() => {});
    await page.waitForTimeout(1500);
}

// Expand every collapsed group in the DX NavBar so that all links are reachable.
async function expandAllNavGroups(page) {
    // DX NavBar group headers have class dxnb-header with a collapse arrow
    // that toggles when clicked. Expanded groups have class dxnb-item-expanded
    // or similar; collapsed ones have dxnb-item-collapsed. We click any header
    // that looks collapsed.
    const clicked = await page.evaluate(() => {
        const headers = [...document.querySelectorAll('div.dxnb-header, table.dxnb-header, .dxnb-item > .dxnb-header')];
        let n = 0;
        for (const h of headers) {
            const item = h.closest('.dxnb-item') || h.parentElement;
            if (!item) continue;
            const cls = item.className || '';
            // Expand any that are not already marked expanded
            if (!/dxnb-item-expanded|dxnbExpanded/.test(cls)) {
                h.click();
                n++;
            }
        }
        return n;
    });
    console.log(`  expanded ${clicked} nav groups`);
    await page.waitForTimeout(800);
}

// Click a navigation item by its visible text. Search both DX NavBar links
// and DX TreeView nodes. Native-click via evaluate to bypass visibility checks.
async function navTo(page, label) {
    console.log(`> nav: ${label}`);
    const matched = await page.evaluate((wanted) => {
        const rxEsc = wanted.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
        const all = [
            ...document.querySelectorAll('a.dxnb-link'),
            ...document.querySelectorAll('.dxtv-nd'),
        ];
        const exact = all.find(a => (a.innerText || '').trim() === wanted);
        if (exact) { exact.click(); return exact.innerText.trim(); }
        const ci = new RegExp('^\\s*' + rxEsc + '\\s*$', 'i');
        const caseI = all.find(a => ci.test(a.innerText || ''));
        if (caseI) { caseI.click(); return caseI.innerText.trim(); }
        const contains = all.find(a => (a.innerText || '').toLowerCase().includes(wanted.toLowerCase()));
        if (contains) { contains.click(); return contains.innerText.trim(); }
        return null;
    }, label);
    if (!matched) {
        throw new Error(`nav link not found: ${label}`);
    }
    await page.waitForLoadState('networkidle', { timeout: 30000 }).catch(() => {});
    await page.waitForTimeout(1500);
}

// Open the first row of the current list view (double-click the data cell).
async function openFirstRow(page) {
    const row = page.locator('tr[class*="dxgvDataRow"]').first();
    if (await row.count() === 0) {
        console.log('  no rows to open');
        return false;
    }
    await row.dblclick({ force: true });
    await page.waitForLoadState('networkidle', { timeout: 30000 }).catch(() => {});
    await page.waitForTimeout(1500);
    return true;
}

// Click a tab by label. DX tab strip uses .dxtc-tab spans inside .dxtc containers.
async function clickTab(page, label) {
    const clicked = await page.evaluate((wanted) => {
        const candidates = [
            ...document.querySelectorAll('.dxtc-tab, .dxtcTab, .dxtc-tabContainer a, .dxtc a'),
        ];
        const rx = new RegExp(wanted, 'i');
        const hit = candidates.find(e => rx.test(e.innerText || ''));
        if (hit) { hit.click(); return (hit.innerText || '').trim(); }
        return null;
    }, label);
    if (!clicked) {
        console.log(`  tab not found: ${label}`);
        return false;
    }
    await page.waitForTimeout(1000);
    return true;
}

async function listTabs(page) {
    const names = await page.evaluate(() => {
        const candidates = [
            ...document.querySelectorAll('.dxtc-tab, .dxtcTab, .dxtc-tabContainer a, .dxtc a'),
        ];
        return [...new Set(candidates.map(e => (e.innerText || '').trim()).filter(Boolean))];
    });
    console.log(`  tabs on current view: ${JSON.stringify(names)}`);
    return names;
}

// Click a "New" action on the list view toolbar.
async function clickNew(page) {
    const newBtn = page.locator('a, span, div', { hasText: /^\s*New\s*$/ }).first();
    await newBtn.click({ timeout: 8000 }).catch(() => {});
    await page.waitForTimeout(1200);
}

// Close the current detail view and return to list, using Close / toolbar cancel.
async function closeView(page) {
    const closeBtn = page.locator('a, span, div', { hasText: /^\s*Close\s*$/ }).first();
    if (await closeBtn.count() > 0) {
        await closeBtn.click({ timeout: 3000 }).catch(() => {});
        await page.waitForTimeout(800);
        // Dismiss any "discard changes" dialog
        const yes = page.getByText(/^\s*OK\s*$|^\s*Yes\s*$/).first();
        if (await yes.count() > 0) {
            await yes.click({ timeout: 2000 }).catch(() => {});
            await page.waitForTimeout(500);
        }
    }
}

async function safe(label, fn) {
    try {
        await fn();
    } catch (err) {
        console.log(`  ! failed: ${label}: ${err.message.split('\n')[0]}`);
    }
}

(async () => {
    const browser = await chromium.launch({ headless: true });
    const context = await browser.newContext({ viewport: { width: 1600, height: 900 } });
    const page = await context.newPage();

    await login(page);
    await shoot(page, '_post-login');
    await expandAllNavGroups(page);

    // --- NAV PANE: clip the left side of the post-login screen
    await safe('nav-pane', async () => {
        await shoot(page, 'nav-pane', { clip: { x: 0, y: 0, width: 340, height: 900 } });
    });

    // --- EQUIPMENT LIST
    await safe('equipment-list', async () => {
        await navTo(page, 'Equipment');
        await shoot(page, 'equipment-list');
        await shoot(page, 'list-view');  // reuse same shot as the generic list-view example
    });

    // --- EQUIPMENT DETAIL
    await safe('equipment-detail', async () => {
        if (await openFirstRow(page)) {
            await shoot(page, 'equipment-detail');
            await shoot(page, 'detail-view');
            await closeView(page);
        }
    });

    // --- LOCATION SETUP (Areas)
    await safe('location-setup', async () => {
        await navTo(page, 'Areas');
        await shoot(page, 'location-setup');
    });

    // --- EQUIPMENT CLASS
    await safe('eq-class', async () => {
        await navTo(page, 'Equipment Classes');
        await shoot(page, 'eq-class');
    });

    // --- PM SCHEDULE
    await safe('pm-schedule', async () => {
        await navTo(page, 'PM Schedule');
        if (await openFirstRow(page)) {
            await shoot(page, 'pm-schedule');
            await closeView(page);
        }
    });

    // --- PM PATCH
    await safe('pm-patch', async () => {
        await navTo(page, 'PM Batches');
        await shoot(page, 'pm-patch');
    });

    // --- WORK REQUEST
    await safe('wr-detail', async () => {
        await navTo(page, 'Work Request');
        if (await openFirstRow(page)) {
            await shoot(page, 'wr-detail');
            await closeView(page);
        }
    });

    await safe('wr-new', async () => {
        await navTo(page, 'Work Request');
        await clickNew(page);
        await shoot(page, 'wr-new');
        await closeView(page);
    });

    // --- WORK ORDER (CM and PM are tree nodes; reports come last as fallback)
    await safe('wo-header', async () => {
        for (const label of ['CM Work Order', 'PM Work Order', 'Work Full List']) {
            try { await navTo(page, label); } catch { continue; }
            if (await openFirstRow(page)) {
                await shoot(page, 'wo-header');
                await listTabs(page);
                if (await clickTab(page, 'Equipment List')) await shoot(page, 'wo-operations');
                if (await clickTab(page, 'Man Hours')) await shoot(page, 'wo-manhours');
                if (await clickTab(page, 'Job Status List')) await shoot(page, 'wo-jobstatus');
                if (await clickTab(page, 'Purchase Request')) await shoot(page, 'pr-from-wo');
                await closeView(page);
                return;
            }
        }
        console.log('  all WO nav targets were empty');
    });

    // --- PURCHASE REQUEST
    await safe('pr-detail', async () => {
        await navTo(page, 'Purchase Requests');
        if (await openFirstRow(page)) {
            await shoot(page, 'pr-detail');
            await shoot(page, 'pr-post-sap');  // same view shows the Post to SAP action
            await closeView(page);
        }
    });

    // --- DEVIATION (the All-Status view is a treeview node under the Deviation group)
    await safe('deviation', async () => {
        const clicked = await page.evaluate(() => {
            const nodes = [...document.querySelectorAll('.dxtv-nd, a.dxnb-link')];
            const target = nodes.find(n => /Deviation.*All Status/i.test(n.innerText || ''));
            if (target) { target.click(); return true; }
            return false;
        });
        if (!clicked) throw new Error('Deviation (All Status) not found');
        await page.waitForLoadState('networkidle', { timeout: 20000 }).catch(() => {});
        await page.waitForTimeout(1500);
        if (await openFirstRow(page)) {
            await shoot(page, 'deviation-new');
            await shoot(page, 'deviation-workflow');
            await listTabs(page);
            if (await clickTab(page, 'Risk Assessment')) await shoot(page, 'deviation-risk');
            await closeView(page);
        }
    });

    // --- REPORT PREVIEW (run the first report in the Reports group)
    await safe('report-preview', async () => {
        const clicked = await page.evaluate(() => {
            const candidates = [...document.querySelectorAll('a.dxnb-link, .dxtv-nd')];
            const r = candidates.find(c => /monthly kpi|bad actor|weekly ams|status report|layout/i.test(c.innerText || ''));
            if (r) { r.click(); return (r.innerText || '').trim(); }
            return null;
        });
        if (!clicked) { console.log('  no report link found'); return; }
        console.log(`  running report: ${clicked}`);
        await page.waitForLoadState('networkidle', { timeout: 30000 }).catch(() => {});
        await page.waitForTimeout(3500);
        await shoot(page, 'report-preview');
    });

    // --- ADMIN: System Users, Role, Job Statuses, Contractor
    await safe('admin-users', async () => {
        await navTo(page, 'System Users');
        await shoot(page, 'admin-users');
        if (await openFirstRow(page)) {
            await shoot(page, 'admin-role');
            await closeView(page);
        }
    });

    await safe('job-status-setup', async () => {
        await navTo(page, 'Job Statuses');
        await shoot(page, 'job-status-setup');
    });

    await safe('contractor-setup', async () => {
        await navTo(page, 'Contractor');
        await shoot(page, 'contractor-setup');
    });

    // --- REPORTS
    await safe('reports', async () => {
        await navTo(page, 'Report');
        await shoot(page, 'report-list');
    });

    console.log('\nDone. Screenshots under', SHOTS_DIR);
    await browser.close();
})().catch(err => {
    console.error(err);
    process.exit(1);
});
