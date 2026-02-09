using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Playwrightdemo;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {

        //Playwright
        // 'await' keyword requires async method -> use 'async' in method header
        using var playwright = await Playwright.CreateAsync();
        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        //Creates new page
        var page = await browser.NewPageAsync();
        //Goes to specified site
        await page.GotoAsync(url: "http://www.eaapp.somee.com");
        //Click 'Login' button
        await page.ClickAsync(selector:"text=Login");
        //Takes a screenshot of completed action
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAApp.jpg"
        });

        Assert.Pass();
    }
}
