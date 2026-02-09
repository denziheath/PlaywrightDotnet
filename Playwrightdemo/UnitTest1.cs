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
        await using var browser = await playwright.Chromium.LaunchAsync();
        //Page
        var page = await browser.NewPageAsync();
        await page.GotoAsync(url: "http://www.somewebsite.com");
        await page.ClickAsync(selector:"text=Login");

        Assert.Pass();
    }
}
