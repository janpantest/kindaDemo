import { expect, Locator, Page } from '@playwright/test';
import { firstRunValues, secondRunValues } from '../helpers/constants';
import { clickIfElementClickable, isButtonClickable } from '../helpers/helpers';
import fs from 'fs';

export class DemoqaHomePage {
    readonly page: Page;
    readonly logoHome: Locator;
    readonly inputBox: Locator;
    readonly result: Locator;
    readonly dropdown: Locator;
    readonly selectValue: Locator;
    readonly buttonPrevious: Locator;
    readonly buttonNext: Locator;

    constructor(page: Page) {
        this.page = page;
        this.logoHome = this.page.locator('div#app header a');
        this.inputBox = this.page.locator('input#searchBox');
        this.result = this.page.locator('span a');
        this.dropdown = this.page.getByLabel('rows per page');
        this.selectValue = this.page.locator('span select option');
        this.buttonPrevious = this.page.getByRole('button', { name: 'Previous' });
        this.buttonNext = this.page.getByRole('button', { name: 'Next' });
    }

    async goToHome(url: string): Promise<void> {
        await this.page.goto(url);
    }

    async checkHomePage(): Promise<void> {
        await expect(this.logoHome).toBeVisible();
        await expect(this.inputBox).toBeVisible();

    }

    async searchForBoook(title: string): Promise<void> {
        await this.inputBox.fill(title);
    }

    async checkResult(title: string): Promise<void> {
        await expect(this.result).toBeVisible();
        await expect(this.result).toHaveText(title);
    }

    async checkNumberOfRows(): Promise<void> {
        const numberOfRows = await this.result.all();
        expect(numberOfRows.length).toEqual(1);
    }

    async goToBookDetail(nthElement: number): Promise<void> {
        await expect(this.result.nth(nthElement)).toBeVisible();
        await this.result.nth(nthElement).click();
    }

    async checkUrl(expectedUrl: string): Promise<void> {
        await expect(this.page).toHaveURL(expectedUrl);
    }

    async changeNumberOfRows(): Promise<void> {
        await expect(this.dropdown).toBeVisible();
        await this.dropdown.click();
        await this.dropdown.selectOption('5');
        // await this.page.waitForTimeout(1000);
    }

    async clickIfEnabled(locator: 'Next'| 'Previous'): Promise<void> {
        (locator === 'Next') ? await clickIfElementClickable(this.buttonNext) : await clickIfElementClickable(this.buttonPrevious) 
    }

    async clickIfEnabledTwo(): Promise<void> {
        await clickIfElementClickable(this.buttonPrevious)
    }

        async clickIfEnabledThree(): Promise<void> {
        (await isButtonClickable(this.buttonPrevious) === true) ? await this.buttonPrevious.click() : await this.buttonNext.click();
    }

    async clickNextButton(): Promise<any> {
        // await this.page.waitForTimeout(2000);
        const bookTitle = await this.result.first().textContent();
        console.info(`Book title: ${bookTitle}`);
        fs.writeFileSync('extractedBookTitle.txt', bookTitle || 'No title found', 'utf-8');
        return bookTitle;
    }

    async clickPreviousButton(titleOne: string): Promise<void> {
        // await this.page.waitForTimeout(1000);
        await expect(this.result.first()).toBeVisible();
        const bookTitle = await this.result.first().textContent();
        expect(bookTitle).not.toEqual(titleOne);
    }

    async getBookList(run: 'First' | 'Second'): Promise<void> {
        await expect(this.result.nth(2)).toBeVisible();

        const amount = await this.result.count();

        for (let i = 0; i < amount ; i++) {
            // console.info(await this.result.nth(i).textContent());

            (run === 'First') ? expect(await this.result.nth(i).textContent()).toEqual(firstRunValues[i]) : expect(await this.result.nth(i).textContent()).toEqual(secondRunValues[i]);
        }
    }
}