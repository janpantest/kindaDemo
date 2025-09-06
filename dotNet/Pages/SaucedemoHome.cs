using Microsoft.Playwright;
using System.Threading.Tasks;  
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System;

namespace Dotnet.Pages
{
    public class SaucedemoHome
    {
        private IPage _page;
        public SaucedemoHome(IPage page) => _page = page;
        private ILocator _usernameInput => _page.Locator("#user-name");
        private ILocator _passwordInput => _page.Locator("#password");

        public async Task CheckVisibilityLoginInputs(string username, string password)
        {
            await Expect(_usernameInput).ToBeVisibleAsync();
            await Expect(_passwordInput).ToBeVisibleAsync();
        }

        public async Task Login(string username, string password)
        {
            await Expect(_usernameInput).ToBeVisibleAsync();
            await Expect(_passwordInput).ToBeVisibleAsync();
            await _usernameInput.FillAsync(username);
            await _passwordInput.FillAsync(password);
            await _page.Locator("#login-button").ClickAsync();
        }
    }

}