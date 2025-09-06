using Microsoft.Playwright;
using System.Threading.Tasks;  
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System;

namespace Dotnet.Pages
{
    public class SaucedemoCartDetail
    {
        private IPage _page;
        public SaucedemoCartDetail(IPage page) => _page = page;
        private ILocator _removeButton => _page.Locator("button[id*='remove']");
        private ILocator _continueButton => _page.Locator("button[id*='continue-shopping']");

        public async Task RemoveProductFromCart()
        {
            await Expect(_removeButton).ToBeVisibleAsync();
            await _removeButton.ClickAsync();
        }

        public async Task VerifyButtons()
        {
            await Expect(_continueButton).ToBeVisibleAsync();
            await Expect(_removeButton).ToBeVisibleAsync();
        }

        public async Task ContinueShopping()
        {
            await Expect(_continueButton).ToBeVisibleAsync();
            await _continueButton.ClickAsync();
        }

        public async Task OpenCart()
        {
            await _page.Locator("a.shopping_cart_link").ClickAsync();
        }
    }
}