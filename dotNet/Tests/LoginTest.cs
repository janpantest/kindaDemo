using NUnit.Framework;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Dotnet.Pages;  // For IceLoginPageUpdated
using System.Threading.Tasks;

namespace Dotnet
{
    public class UpdatedTest : PageTest
    {
        [Test]
        public async Task LoginTest()
        {
            await Page.GotoAsync("https://icehrmpro.gamonoid.com/login.php?");

            var loginPage = new IceLoginPageUpdated(Page);
            await loginPage.FillUserInput("admin");
            // await loginPage.FillPassword("admin");
            // await loginPage.ClickLogin();

            // Optionally wait or assert
            await Page.WaitForTimeoutAsync(2000);

            // Logging
            TestContext.WriteLine("LoginTest: Test completed.");
        }
    }
}
