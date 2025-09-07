import { test } from '@playwright/test';
import * as demoqaSteps from '../../steps/demoqa.steps';

const url = '/books';
const title = 'Git Pocket Guide';

test('Search for book', { tag: '@searchBook' }, async ({ page }) => {
    await demoqaSteps.checkDemoqaHome(page, url);

    // await demoqaSteps.checkDemoqaHome(page, url);
    await demoqaSteps.searchForBoook(page, title);
    await demoqaSteps.checkResults(page, title);
    await demoqaSteps.checkNumberOfRows(page);
});

test('Get book\'s detail', { tag: '@getDetail' }, async ({ page }) => {
    const expectedUrl = 'https://demoqa.com/books?book=9781449337711';

    await demoqaSteps.checkDemoqaHome(page, url);
    await demoqaSteps.getBookDetail(page, 2);
    await demoqaSteps.checkUrl(page, expectedUrl);
});

test('Pagination', { tag: '@pagination' }, async ({ page }) => {
    await demoqaSteps.checkDemoqaHome(page, url);
    await demoqaSteps.changeNumberOfRows(page);
    await demoqaSteps.getBookList(page, 'First');
    const titleOne = await demoqaSteps.clickNextButton(page);
    await demoqaSteps.getBookList(page, 'Second');
    await demoqaSteps.clickPreviousButton(page, titleOne);
});

// npx playwright test demoqa.test.ts --headed --project=chromium