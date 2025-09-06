using Microsoft.Playwright;
using System.Threading.Tasks;  // <-- Add this to resolve Task type
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Dotnet.Pages
{
    public class IceLoginPage  // Ensure the class name is IceLoginPage
    {
        private IPage _page;
        private readonly ILocator _userInput;
        private readonly ILocator _passwordInput;
        private readonly ILocator _loginButton;

        // Corrected constructor name to match the class name
        public IceLoginPage(IPage page)
        {
            _page = page;
            _userInput = _page.Locator("input#username");
            _passwordInput = _page.Locator("input#password");
            _loginButton = _page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
        }

        // public async Task CheckLoginStuff()
        // {
        //     await Expect(_userInput).ToBeVisible();
        //     await Expect(_passwordInput).ToBeVisible();
        //     await Expect(_loginButton).ToBeVisible();
            
        // }

        // Fill the username input field
        public async Task FillUserInput(string userName)
        {
            await _userInput.FillAsync(userName);
            // await _page.WaitForTimeoutAsync(1000);
        }

        // Fill the password input field
        public async Task FillPassword(string password)
        {
            await _passwordInput.FillAsync(password);
            // await _page.WaitForTimeoutAsync(1000);
        }

        // Click the login button
        public async Task ClickLogin()
        {
            await _loginButton.ClickAsync();
        }
    }
}
