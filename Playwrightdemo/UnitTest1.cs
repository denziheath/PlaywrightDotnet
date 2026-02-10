using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Playwrightdemo.Pages;

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
            //Displays browser window when performing test
            Headless = false
        });
        //Creates new page
        var page = await browser.NewPageAsync();
        //Goes to specified site
        await page.GotoAsync(url: "http://www.eaapp.somee.com");
        //Click specified button, in this case 'Login' button
        await page.ClickAsync(selector: "text=Login");
        //Takes a screenshot of completed action
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAApp.jpg"
        });
        //Perform entry of data into a text box
        //'selector' parameter identifies the element
        await page.FillAsync(selector: "#UserName", value: "admin");
        await page.FillAsync(selector: "#Password", value: "password");
        await page.ClickAsync(selector: "text=Log in");
        //Verify if link exists
        var isExist = await page.Locator(selector: "text='Employee Details'").IsVisibleAsync();
        //'That' condition is new version of IsTrue (deprecated)
        Assert.That(isExist);
    }



    [Test]
    public async Task TestWithPOM()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        var page = await browser.NewPageAsync();
        await page.GotoAsync(url: "http://www.eaapp.somee.com");

        //Can swap LoginPageUpgraded with LoginPage
        var loginPage = new LoginPageUpgraded(page);
        await loginPage.ClickLogin();
        await loginPage.Login(userName: "admin", password: "password");
        var isExist = await loginPage.IsEmployeeDetailsExists();
        Assert.That(isExist);
    }



    [Test]
    public async Task WaitTest()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
          //SlowMo = 1000
        });
        var content = await browser.NewContextAsync();
        var page = await content.NewPageAsync();
        await page.GotoAsync(url: "https://demos.telerik.com/kendo-ui/window/index");

        //How to handle buttons w/o text and aria label
        //Opens and closes window in browser
        await page.GetByRole(AriaRole.Button, new() { Name = "Close" }).ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Close" }).ClickAsync();

        await page.GetByText("Click here to open the window.").ClickAsync();
    }


    [Test]
    public async Task TestNetwork()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        var page = await browser.NewPageAsync();
        await page.GotoAsync(url: "http://www.eaapp.somee.com");

        var loginPage = new LoginPageUpgraded(page);
        await loginPage.ClickLogin();
        await loginPage.Login(userName: "admin", password: "password");

        //Get more details from api call using WaitForResponseAsync opposed to WaitForRequestAsync
        //var waitResponse = page.WaitForResponseAsync("**/Employee");
        //await loginPage.ClickEmployeeList();
        //var getResponse = await waitResponse;

        var response = await page.RunAndWaitForResponseAsync(async () =>
        {
            await loginPage.ClickEmployeeList();
        }, x => x.Url.Contains("/Employee"));

        var isExist = await loginPage.IsEmployeeDetailsExists();
        Assert.That(isExist);
    }
}