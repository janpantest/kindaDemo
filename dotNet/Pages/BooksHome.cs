using Microsoft.Playwright;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System;
using Dotnet.Data;

namespace Dotnet.Pages
{
    public class BooksHome
    {
        private IPage _page;
        public BooksHome(IPage page) => _page = page;

        private ILocator _header => _page.Locator("header a");
        private ILocator _textBox => _page.GetByRole(AriaRole.Textbox, new() { Name = "Type to search" });
        private ILocator _rowsPerPage => _page.GetByLabel("rows per page");
        private ILocator _row => _page.Locator("span a");
        // First and second run values as arrays
        // private readonly string[] firstRunValues = new string[] 
        // {
        //     "Git Pocket Guide", 
        //     "Learning JavaScript Design Patterns", 
        //     "Designing Evolvable Web APIs with ASP.NET", 
        //     "Speaking JavaScript", 
        //     "You Don't Know JS"
        // };

        // private readonly string[] secondRunValues = new string[] 
        // {
        //     "Programming JavaScript Applications", 
        //     "Eloquent JavaScript, Second Edition", 
        //     "Understanding ECMAScript 6"
        // };

        private ILocator _result => _page.Locator("span a"); // Adjust the selector as per your use case



        public async Task CheckTitle()
        {
            await Expect(_header).ToBeVisibleAsync();
        }

        public async Task EnterBook(string bookName)
        {
            await Expect(_textBox).ToBeVisibleAsync();
            await _textBox.FillAsync(bookName);
        }

        public async Task GetRowCount()
        {
            var rows = await _row.AllAsync();  // Returns a list of ILocator
            // Console.WriteLine($"No of rows getrowcount: {rows.Count}");  // This will give you the correct count

            Assert.AreEqual(1, rows.Count);  // Use Assert to check the count of rows
        }

        public async Task ChangeNumberOfRow()
        {
            await _rowsPerPage.SelectOptionAsync(new[] { "5" });
        }


        // needs to be checked 
        public async Task GetBookListAsync(string run)
        {
            // Ensure that the 3rd element is visible (nth(2) in JavaScript is equivalent to nth-child(3) in Playwright)
            await Expect(_result.Nth(2)).ToBeVisibleAsync();

            var amount = await _result.CountAsync();
            var allTextContents = await _result.AllTextContentsAsync();
            Console.WriteLine("blabla : " + await _result.AllTextContentsAsync());

            foreach (var textContent in allTextContents)
            {
                Console.WriteLine(textContent);  // Prints each element's text
            }

            for (int i = 0; i < amount; i++)
            {
                var textContent = await _result.Nth(i).TextContentAsync();
                Console.WriteLine(textContent);

                // Check if the value should be from the first or second run
                if (run == "First")
                {
                    // Assert that the text matches the corresponding value from firstRunValues
                    Console.WriteLine("First run");
                    Assert.AreEqual(RunValues.FirstRunValues[i], textContent?.Trim());
                }
                else if (run == "Second")
                {
                    // Assert that the text matches the corresponding value from secondRunValues
                    Console.WriteLine("SEcond run");
                    Assert.AreEqual(RunValues.SecondRunValues[i], textContent?.Trim());
                }
                else
                {
                    Assert.Fail("Invalid 'run' parameter. It should be either 'First' or 'Second'.");
                }
            }
        }
    }
}
