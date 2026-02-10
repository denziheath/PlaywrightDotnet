using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace Playwrightdemo.Pages;


public class LoginPageUpgraded
{
    //Playwright best practices dictate creating a constructor
    //IPage is responsible for holding all page details

    private IPage _page;
    
    //Constructor initialization moved to top
    // => is used in lieu of {  } 
    public LoginPageUpgraded(IPage page) => _page = page;

    private ILocator _lnkLogin => _page.Locator(selector: "text=Login");
    private ILocator _txtUserName => _page.Locator(selector: "#UserName");
    private ILocator _txtPassword => _page.Locator(selector: "#Password");
    private ILocator _btnLogin => _page.Locator(selector: "text=Log in");
    private ILocator _lnkEmployeeDetails => _page.Locator(selector: "text='Employee Details'");
    private ILocator _lnkEmployeeLists => _page.Locator(selector: "text='Employee List'");

    public async Task ClickLogin()
    {

        await _page.RunAndWaitForNavigationAsync(async () =>
        {
            await _lnkLogin.ClickAsync();
        }, new PageRunAndWaitForNavigationOptions
        {
            //Wait for specific location in url 
            UrlString = "**/Login"
        });
    }

    public async Task ClickEmployeeList() => await _lnkEmployeeLists.ClickAsync();

    public async Task Login(string userName, string password)
    {
        await _txtUserName.FillAsync(userName);
        await _txtPassword.FillAsync(password);
        await _btnLogin.ClickAsync();
    }

    public async Task<bool> IsEmployeeDetailsExists() => await _lnkEmployeeDetails.IsVisibleAsync();
}