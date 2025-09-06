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
    public class Books: PageTest
    {
        [SetUp] 
        public void Setup()
        {
            // Get the base directory of the application (project root directory)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Move up one directory to the project root
            string projectRootDir = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, @"..\..\..\"));

            // Construct the path to the .env file dynamically from the project root
            string envPath = System.IO.Path.Combine(projectRootDir, ".env");

            // Log the path being used
            Console.WriteLine($"Loading .env file from: {envPath}");

            // Load the .env file
            Env.Load(envPath);
        }

        [Test]
        public async Task BooksFlow()
        {
            string baseUrl = Environment.GetEnvironmentVariable("BOOK_URL");
            await Page.GotoAsync(baseUrl);

            var bookHome = new BooksHome(Page);
            await bookHome.CheckTitle();
            await bookHome.ChangeNumberOfRow();
            // await Page.WaitForTimeoutAsync(2000);
            // await bookHome.GetBookListAsync("First");
            // await Page.WaitForTimeoutAsync(2000);
            await bookHome.EnterBook("Git Pocket");
            await bookHome.GetRowCount();

            await Page.WaitForTimeoutAsync(5000);

        }
    }
}