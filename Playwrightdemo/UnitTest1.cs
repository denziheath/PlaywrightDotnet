//using System.Threading.Tasks;
//using Microsoft.Playwright;
//using NUnit.Framework;

//namespace Playwrightdemo;

//public class Tests
//{
//    [SetUp]
//    public void Setup()
//    {
//    }

//    [Test]
//    public async Task Test1()
//    {

//        //Playwright
//        // 'await' keyword requires async method -> use 'async' in method header
//        using var playwright = await Playwright.CreateAsync();
//        //Browser
//        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
//        {
//            //Displays browser window when performing test
//            Headless = false
//        });
//        //Creates new page
//        var page = await browser.NewPageAsync();
//        //Goes to specified site
//        await page.GotoAsync(url: "http://www.eaapp.somee.com");
//        //Click specified button, in this case 'Login' button
//        await page.ClickAsync(selector: "text=Login");
//        //Takes a screenshot of completed action
//        await page.ScreenshotAsync(new PageScreenshotOptions
//        {
//            Path = "EAApp.jpg"
//        });
//        //Perform entry of data into a text box
//        //'selector' parameter identifies the element
//        await page.FillAsync(selector: "#UserName", value: "admin");
//        await page.FillAsync(selector: "#Password", value: "password");
//        await page.ClickAsync(selector: "text=Log in");
//        //Verify if link exists
//        var isExist = await page.Locator(selector: "text='Employee Details'").IsVisibleAsync();
//        //'That' condition is new version of IsTrue (deprecated)
//        Assert.That(isExist);
//    }
//}