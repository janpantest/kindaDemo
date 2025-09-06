// Helpers/PlaywrightHelpers.cs
using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using System.Text;
using System.IO;

namespace Dotnet.Helpers
{
    public static class PlaywrightHelpers
    {
        public static async Task ClickIfElementExistAsync(ILocator locator, int timeout = 5000)
        {
            bool elementVisible = await IsElementExistsAsync(locator, timeout);
            if (elementVisible)
            {
                await locator.ClickAsync();
            }
        }

        public static async Task<bool> IsElementExistsAsync(ILocator locator, int timeout = 5000)
        {
            try
            {
                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = timeout
                });
                return true;
            }
            catch (TimeoutException)
            {
                return false;
            }
        }

        public static async Task GetLocatorTextAsync(ILocator locator)
        {
            try
            {
                int count = await locator.CountAsync();
                Console.WriteLine($"Locator Count: {count}");
                for (int i = 0; i < count; i++)
                {
                    string itemText = await locator.Nth(i).InnerTextAsync();
                    Console.WriteLine($"Item {i} Text: {itemText}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving text: {ex.Message}");
            }
        }

        public static async Task SaveTextToFileAsync(ILocator locator, string filePath)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string projectRoot = Path.Combine(currentDirectory, "..", "..", "..");
                string pathToSaveFile = Path.Combine(projectRoot, filePath);

                int count = await locator.CountAsync();
                Console.WriteLine($"Total Elements Found: {count}");

                // Create a StringBuilder to store text
                StringBuilder sb = new StringBuilder();

                // Loop through the elements and extract text
                for (int i = 0; i < count; i++)
                {
                    string itemText = await locator.Nth(i).InnerTextAsync();
                    if (!string.IsNullOrWhiteSpace(itemText))  // Filter out empty or whitespace-only text
                    {
                        sb.AppendLine($"Element {i} Text: {itemText}");
                    }
                }

                // Save the collected text to a file
                await File.WriteAllTextAsync(pathToSaveFile, sb.ToString());
                Console.WriteLine($"Text saved to file: {pathToSaveFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving or saving text: {ex.Message}");
            }
        }
    }
}
