using Microsoft.Playwright;
using System.Threading.Tasks;  
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System;

namespace Dotnet.Pages
{
    public class IceLoginPageUpdated  
    {
        private IPage _page;
        public IceLoginPageUpdated(IPage page) => _page = page;

        private ILocator _userInput => _page.Locator("input#username");
        private ILocator _passwordInput => _page.Locator("input#password");
        private ILocator _loginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Log in" });

        // }

        // Fill the username input field
        public async Task FillUserInput(string userName)
        {
            await _userInput.FillAsync(userName);
        }

        // Fill the password input field
        public async Task FillPassword(string password)
        {
            await _passwordInput.FillAsync(password);
        }

        // Click the login button
        public async Task ClickLogin()
        {
            await _loginButton.ClickAsync();
        }

        public async Task CheckLoginElements()
        {
            await Expect(_userInput).ToBeVisibleAsync();
            await Expect(_passwordInput).ToBeVisibleAsync();
            await Expect(_loginButton).ToBeVisibleAsync();
        }
    }
}
