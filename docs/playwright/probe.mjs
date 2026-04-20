import { chromium } from '@playwright/test';

const BASE = 'http://localhost/ams';
const USER = 'admin';
const PASS = 'super';

(async () => {
    const browser = await chromium.launch({ headless: true });
    const page = await (await browser.newContext({ viewport: { width: 1600, height: 900 } })).newPage();

    await page.goto(`${BASE}/Login.aspx`, { waitUntil: 'networkidle' });
    await page.fill('input[id$="dviUserName_Edit_I"]', USER);
    await page.fill('input[id$="dviPassword_Edit_I"]', PASS);
    await page.locator('#Logon_PopupActions_Menu').getByText('Log On', { exact: true }).first().click();
    await page.waitForLoadState('networkidle', { timeout: 30000 }).catch(() => {});
    await page.waitForTimeout(1500);

    await page.evaluate(() => {
        const headers = [...document.querySelectorAll('div.dxnb-header, table.dxnb-header, .dxnb-item > .dxnb-header')];
        for (const h of headers) {
            const item = h.closest('.dxnb-item') || h.parentElement;
            if (!item) continue;
            const cls = item.className || '';
            if (!/dxnb-item-expanded|dxnbExpanded/.test(cls)) h.click();
        }
    });
    await page.waitForTimeout(1000);

    const allLinks = await page.evaluate(() =>
        [...document.querySelectorAll('a.dxnb-link')].map(a => (a.innerText || '').trim())
    );
    console.log('All nav links (deduped, sorted):');
    for (const t of [...new Set(allLinks)].sort()) console.log(`  ${t}`);

    console.log('\nTree nodes (dxtv-nd):');
    const treeNodes = await page.evaluate(() =>
        [...document.querySelectorAll('.dxtv-nd')].map(n => (n.innerText || '').trim())
    );
    for (const t of [...new Set(treeNodes)].sort()) console.log(`  ${t}`);

    await browser.close();
})();
