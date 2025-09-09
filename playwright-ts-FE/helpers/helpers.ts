import { Locator, TestInfo } from '@playwright/test';


export async function clickIfElementExist(locator: Locator): Promise<void> {
    const elementVisible = await isElementVisible(locator);
    if (elementVisible) {
        await locator.click();
    }
}

export async function isElementVisible(locator: Locator, timeout?: number): Promise<boolean> {
    try {
        await locator.waitFor({ state: 'visible', timeout: timeout ?? 5000 });
        return true;
    }
    catch {
        return false;
    }
}

export async function clickIfElementClickable(locator: Locator): Promise<void> {
    const elementEnabled = await isButtonClickable(locator);
    if (elementEnabled) {
        await locator.click();
    }
}

export async function isButtonClickable(locator: Locator, timeout?: number): Promise<boolean> {
    try {
        const isEnabled = await locator.isEnabled();
        return isEnabled;
    } 
    catch {
        return false;
    }
}


/**
 * Returns the baseURL from the Playwright config at runtime.
 * Needs to be called inside a test or step that has access to `testInfo`.
 */
export function getBaseURL(testInfo: import('@playwright/test').TestInfo): string {
// export function getBaseURL(): string {
    const baseURL = testInfo.project.use.baseURL;
    if (!baseURL) {
        throw new Error('baseURL is not defined in playwright.config.ts');
    }
    return baseURL;
}
