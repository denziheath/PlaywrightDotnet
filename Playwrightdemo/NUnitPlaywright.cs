using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Playwrightdemo;

//Compare to UnitTest1 - easier way to do same job (w/o screenshot) 


//Inheriting the 'PageTest' class allows us to remove the Playwright Async calls
public class NUnitPlaywright : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        //Navigating to page moved to Setup - not really explained why
        await Page.GotoAsync(url: "http://www.eaapp.somee.com");
    }

    [Test]
    public async Task Test1()
    {
        await Page.ClickAsync(selector:"text=Login");
        await Page.FillAsync(selector: "#UserName", value: "admin");
        await Page.FillAsync(selector: "#Password", value: "password");
        await Page.ClickAsync(selector: "text=Log in");
        //The 'Expect' and 'ToBeVisibleAsync' takes place of the 'isExist' and 'Assert'
        await Expect(Page.Locator(selector:"text='Employee Details'")).ToBeVisibleAsync();

    }
}
