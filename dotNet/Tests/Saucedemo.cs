using NUnit.Framework;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Dotnet.Pages;
using System.Threading.Tasks;
using DotNetEnv;
using System;
using System.Collections;

namespace Dotnet
{
    public class Saucedemo: PageTest
    {
        [SetUp]
        public void Setup()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRootDir = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, @"..\..\..\"));
            string envPath = System.IO.Path.Combine(projectRootDir, ".env");
            Console.WriteLine($"Loading .env file from: {envPath}");
            Env.Load(envPath);
        }

        [Test]
        public async Task SaucedemoTest()
        {
            var username = Environment.GetEnvironmentVariable("SAUCEDEMO_USERNAME");
            var password = Environment.GetEnvironmentVariable("SAUCEDEMO_PASSWORD");
            var url = Environment.GetEnvironmentVariable("SAUCEDEMO_URL");
            string filePath = "extracted_product_text.txt";

            var saucedemoHome = new SaucedemoHome(Page);
            var saucedemoProductPage = new SaucedemoProducts(Page);
            var saucedemoCartDetail = new SaucedemoCartDetail(Page);

            await Page.GotoAsync(url);
            await saucedemoHome.CheckVisibilityLoginInputs(username, password);
            await saucedemoHome.Login(username, password);
            await saucedemoProductPage.GetProductTitle(0);
            await saucedemoProductPage.StoreProductTitleToFile(filePath);
            await saucedemoProductPage.AddProductToCart(0);
            await saucedemoProductPage.VerifyCartBadge();

            await saucedemoCartDetail.OpenCart();
            await saucedemoCartDetail.VerifyButtons();
            await saucedemoCartDetail.ContinueShopping();

            await saucedemoProductPage.RemoveProductFromCart(0);

            // Optionally wait or assert
            await Page.WaitForTimeoutAsync(2000);

            // Logging
            TestContext.WriteLine("SaucedemoTest: Test completed.");
        }
    }
}