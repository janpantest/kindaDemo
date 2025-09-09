import { Page, test } from "@playwright/test";
import { DemoqaHomePage } from "../pages/demoqaHome.page";

export async function checkDemoqaHome(page: Page, url: string): Promise<void> {
    await test.step('Check home page', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.goToHome(url);
        await demoqaHome.checkHomePage();
    })
}

export async function searchForBoook(page: Page, title: string): Promise<void> {
    await test.step('Search for book', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.searchForBoook(title);
    })
}

export async function checkResults(page: Page, title: string): Promise<void> {
    await test.step('Check book', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.checkResult(title);
    })
}

export async function checkNumberOfRows(page: Page): Promise<void> {
    await test.step('Check number of rows', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.checkNumberOfRows();
    })
}

export async function getBookDetail(page: Page, nthElement: number): Promise<void> {
    await test.step('Get book detail', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.goToBookDetail(nthElement);
    })
}

export async function checkUrl(page: Page, expectedUrl: string): Promise<void> {
    await test.step('Check URL', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.checkUrl(expectedUrl);
    })
}

export async function changeNumberOfRows(page: Page): Promise<void> {
    await test.step('Change number of rows', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.changeNumberOfRows();
    })
}

export async function clickNextButton(page: Page): Promise<string> {
    return await test.step('Click next button', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.clickIfEnabledThree();
        return await demoqaHome.clickNextButton();
    })
}

export async function clickPreviousButton(page: Page, titleOne: string): Promise<void> {
    return await test.step('Click previous button', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.clickIfEnabledThree();
        await demoqaHome.clickPreviousButton(titleOne);
    })
}

export async function getBookList(page: Page, run: 'First' | 'Second'): Promise<void> {
    return await test.step('Get list', async () => {
        const demoqaHome = new DemoqaHomePage(page);

        await demoqaHome.getBookList(run);
    })
}