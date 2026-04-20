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
    const title = await page.title().catch(() => '');
    console.log(`  (matched='${matched}', title='${title}')`);
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

// Click a tab by label. The DX client API is unreliable when the page has
// multiple hidden tab controls — SetActiveTab on the wrong control produces
// no visible change. We therefore prefer a DOM click on the VISIBLE tab cell.
async function clickTab(page, label) {
    const clickedVia = await page.evaluate((wanted) => {
        const rx = new RegExp('^\\s*' + wanted.replace(/[.*+?^${}()|[\]\\]/g, '\\$&') + '\\s*$', 'i');
        const isVisible = (el) => {
            if (!el) return false;
            const r = el.getBoundingClientRect();
            if (r.width < 2 || r.height < 2) return false;
            const cs = window.getComputedStyle(el);
            return cs.visibility !== 'hidden' && cs.display !== 'none' && cs.opacity !== '0';
        };

        // Approach 1: find a VISIBLE tab strip cell/link and click its nearest
        // clickable ancestor. DX tab cells are typically td.dxtc-tab or similar.
        const candidates = [
            ...document.querySelectorAll('.dxtc-tab, .dxtcTab, .dxtc-tabContainer a, .dxtc a, .dxtc td'),
        ];
        const visibleHits = candidates.filter(e => rx.test(e.innerText || '') && isVisible(e));
        if (visibleHits.length > 0) {
            // Prefer the largest visible candidate (main tab strip, not a sub-widget)
            visibleHits.sort((a, b) => {
                const ra = a.getBoundingClientRect();
                const rb = b.getBoundingClientRect();
                return (rb.width * rb.height) - (ra.width * ra.height);
            });
            const hit = visibleHits[0];
            const clickable = hit.closest('td, a') || hit;
            clickable.click();
            return 'dom-visible';
        }

        // Approach 2: fall back to the DX client API on any tab control
        try {
            for (const k of Object.keys(window)) {
                let v;
                try { v = window[k]; } catch { continue; }
                if (v && typeof v.GetTabCount === 'function' && typeof v.SetActiveTab === 'function' && typeof v.GetTab === 'function') {
                    // Skip tab controls whose main element isn't in the DOM / visible
                    const mainEl = v.GetMainElement ? v.GetMainElement() : null;
                    if (mainEl && !isVisible(mainEl)) continue;
                    const n = v.GetTabCount();
                    for (let i = 0; i < n; i++) {
                        let tab;
                        try { tab = v.GetTab(i); } catch { continue; }
                        if (!tab) continue;
                        const tabText = (tab.GetText && tab.GetText()) || (tab.name) || '';
                        if (rx.test((tabText || '').trim())) {
                            v.SetActiveTab(tab);
                            return 'dx-api';
                        }
                    }
                }
            }
        } catch (e) { /* fallthrough */ }

        // Approach 3: any matching element (visible or not), click closest td/a
        const anyHit = candidates.find(e => rx.test(e.innerText || ''));
        if (anyHit) {
            const clickable = anyHit.closest('td, a') || anyHit;
            clickable.click();
            return 'dom-any';
        }
        return null;
    }, label);
    if (!clickedVia) {
        console.log(`  tab not found: ${label}`);
        return false;
    }
    console.log(`  tab '${label}' via ${clickedVia}`);
    await page.waitForLoadState('networkidle', { timeout: 10000 }).catch(() => {});
    await page.waitForTimeout(2000);
    // Scroll the tab strip (and its active content) to the top of the viewport
    // so the next screenshot actually captures the switched tab content.
    await page.evaluate((wanted) => {
        const rx = new RegExp('^\\s*' + wanted.replace(/[.*+?^${}()|[\]\\]/g, '\\$&') + '\\s*$', 'i');
        const candidates = [...document.querySelectorAll('.dxtc-tab, .dxtcTab')];
        const hit = candidates.find(e => rx.test(e.innerText || ''));
        if (hit) hit.scrollIntoView({ block: 'start', behavior: 'instant' });
    }, label);
    await page.waitForTimeout(500);
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

// Close the current detail view. In XAF web the toolbar "Close" action is
// typically a menu item with id ending in "_Close" inside the main actions menu.
async function closeView(page) {
    await page.evaluate(() => {
        // Find any DX menu/action item whose caption is "Close" and click it.
        const items = [...document.querySelectorAll('a, div.dxm-item, td.dxm-item, .dxbButton, span')];
        const target = items.find(el => {
            const t = (el.innerText || el.getAttribute('aria-label') || '').trim();
            return /^Close$/i.test(t);
        });
        if (target) target.click();
    });
    await page.waitForTimeout(1200);
    // Dismiss any confirmation dialog
    await page.evaluate(() => {
        const buttons = [...document.querySelectorAll('a, div, td, span, input')];
        const ok = buttons.find(b => /^(OK|Yes|Discard)$/i.test((b.innerText || b.value || '').trim()));
        if (ok) ok.click();
    });
    await page.waitForTimeout(800);
}

// Force a hard return to "home" by going directly to Default.aspx. This resets
// the application state between screenshot groups that might otherwise leave
// modal dialogs / detail views on screen.
async function goHome(page) {
    await page.goto(`${BASE}/Default.aspx`, { waitUntil: 'networkidle' }).catch(() => {});
    await page.waitForTimeout(1500);
    await expandAllNavGroups(page);
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
        await goHome(page);
        await navTo(page, 'Equipment');
        await shoot(page, 'equipment-list');
        await shoot(page, 'list-view');
    });

    // --- EQUIPMENT DETAIL
    await safe('equipment-detail', async () => {
        await goHome(page);
        await navTo(page, 'Equipment');
        if (await openFirstRow(page)) {
            await shoot(page, 'equipment-detail');
            await shoot(page, 'detail-view');
        }
    });

    // --- LOCATION SETUP (Areas)
    await safe('location-setup', async () => {
        await goHome(page);
        await navTo(page, 'Areas');
        await shoot(page, 'location-setup');
    });

    // --- EQUIPMENT CLASS
    await safe('eq-class', async () => {
        await goHome(page);
        await navTo(page, 'Equipment Classes');
        await shoot(page, 'eq-class');
    });

    // --- PM SCHEDULE
    await safe('pm-schedule', async () => {
        await goHome(page);
        await navTo(page, 'PM Schedule');
        if (await openFirstRow(page)) {
            await shoot(page, 'pm-schedule');
        }
    });

    // --- PM PATCH
    await safe('pm-patch', async () => {
        await goHome(page);
        await navTo(page, 'PM Batches');
        await shoot(page, 'pm-patch');
    });

    // --- WORK REQUEST
    await safe('wr-detail', async () => {
        await goHome(page);
        await navTo(page, 'Work Request');
        if (await openFirstRow(page)) {
            await shoot(page, 'wr-detail');
        }
    });

    await safe('wr-new', async () => {
        await goHome(page);
        await navTo(page, 'Work Request');
        await clickNew(page);
        await shoot(page, 'wr-new');
    });

    // --- WORK ORDER — each tab gets its own fresh navigation.
    // DX detail views in this app appear to cache the visible panel, so clicking
    // tabs on a cached view doesn't always produce a re-render. Re-opening the
    // detail for each tab shot is slower but reliable.
    const captureWoTab = async (tabLabel, shotName) => {
        await goHome(page);
        for (const navLabel of ['CM Work Order', 'PM Work Order', 'Work Full List']) {
            try { await navTo(page, navLabel); } catch { continue; }
            if (await openFirstRow(page)) {
                if (tabLabel === null) {
                    await shoot(page, shotName);
                    return true;
                }
                if (await clickTab(page, tabLabel)) {
                    await shoot(page, shotName);
                    return true;
                }
            }
        }
        return false;
    };

    await safe('wo-header', async () => {
        await captureWoTab(null, 'wo-header');
    });
    await safe('wo-operations', async () => {
        await captureWoTab('Equipment List', 'wo-operations');
    });
    await safe('wo-manhours', async () => {
        await captureWoTab('Man Hours', 'wo-manhours');
    });
    await safe('wo-jobstatus', async () => {
        await captureWoTab('Job Status List', 'wo-jobstatus');
    });
    await safe('pr-from-wo', async () => {
        await captureWoTab('Purchase Request', 'pr-from-wo');
    });

    // --- PURCHASE REQUEST
    await safe('pr-detail', async () => {
        await goHome(page);
        await navTo(page, 'Purchase Requests');
        if (await openFirstRow(page)) {
            await shoot(page, 'pr-detail');
            await shoot(page, 'pr-post-sap');
        }
    });

    // --- DEVIATION — each shot starts fresh for the same reason as WO tabs.
    const captureDeviation = async (tabLabel, shotName) => {
        await goHome(page);
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
            if (tabLabel === null) { await shoot(page, shotName); return true; }
            if (await clickTab(page, tabLabel)) { await shoot(page, shotName); return true; }
        }
        return false;
    };

    await safe('deviation-new', async () => { await captureDeviation(null, 'deviation-new'); });
    await safe('deviation-workflow', async () => { await captureDeviation(null, 'deviation-workflow'); });
    await safe('deviation-risk', async () => { await captureDeviation('Risk Assessment', 'deviation-risk'); });

    // --- REPORT PREVIEW (run the first report in the Reports group)
    await safe('report-preview', async () => {
        await goHome(page);
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

    // --- ADMIN: each admin shot starts from a fresh home navigation so that
    // previous closeView() / detail-view state can't bleed into the next shot.
    await safe('admin-users', async () => {
        await goHome(page);
        await navTo(page, 'System Users');
        await shoot(page, 'admin-users');
    });

    await safe('admin-role', async () => {
        await goHome(page);
        await navTo(page, 'Role');
        await shoot(page, 'admin-role');
    });

    await safe('job-status-setup', async () => {
        await goHome(page);
        await navTo(page, 'Job Statuses');
        await shoot(page, 'job-status-setup');
    });

    await safe('contractor-setup', async () => {
        await goHome(page);
        await navTo(page, 'Contractors');
        await shoot(page, 'contractor-setup');
    });

    // --- REPORTS
    await safe('reports', async () => {
        await goHome(page);
        await navTo(page, 'Reports');
        await shoot(page, 'report-list');
    });

    console.log('\nDone. Screenshots under', SHOTS_DIR);
    await browser.close();
})().catch(err => {
    console.error(err);
    process.exit(1);
});
