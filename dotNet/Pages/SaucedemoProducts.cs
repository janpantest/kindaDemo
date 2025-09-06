using Microsoft.Playwright;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System;
using Dotnet.Data;
using Dotnet.Helpers;

namespace Dotnet.Pages
{
    public class SaucedemoProducts
    {
        private IPage _page;
        public SaucedemoProducts(IPage page) => _page = page;
        private ILocator _productTitle => _page.Locator("div.inventory_item_name");
        private ILocator _addToCartButton => _page.Locator("button[id*='add']");
        private ILocator _removeButton => _page.Locator("button[id*='remove']");
        private ILocator _shoppingCartBadge => _page.Locator("span.shopping_cart_badge");

        public async Task GetProductTitle(int nthElement)
        {
            await Expect(_productTitle.Nth(nthElement)).ToBeVisibleAsync();
            var title = await _productTitle.Nth(nthElement).InnerTextAsync();
            Console.WriteLine("Product Title: " + title);
        }

        public async Task StoreProductTitleToFile(string pathToSaveFile)
        {
            // Save text from specific locator (e.g., trash-related text)
            await PlaywrightHelpers.GetLocatorTextAsync(_productTitle);

            // Save all text from the page to a file
            await PlaywrightHelpers.SaveTextToFileAsync(_productTitle, pathToSaveFile);
        }

        public async Task AddProductToCart(int nthElement)
        {
            await Expect(_addToCartButton.Nth(nthElement)).ToBeVisibleAsync();
            await _addToCartButton.Nth(nthElement).ClickAsync();
        }

        public async Task RemoveProductFromCart(int nthElement)
        {
            await Expect(_removeButton.Nth(nthElement)).ToBeVisibleAsync();
            await _removeButton.Nth(nthElement).ClickAsync();
        }

        public async Task VerifyCartBadge()
        {
            await Expect(_shoppingCartBadge).ToBeVisibleAsync();
        }
    }
}