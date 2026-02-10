using System;
using Microsoft.Playwright;

namespace Playwrightdemo.Pages;

public class LoginPage
{
    //Playwright best practices dictate creating a constructor
    //IPage is responsible for holding all page details

	private IPage _page;
    private readonly ILocator _lnkLogin;
    private readonly ILocator _txtUserName;
    private readonly ILocator _txtPassword;
    private readonly ILocator _btnLogin;
    private readonly ILocator _lnkEmployeeDetails;

    //Initialization of the constructors
    public LoginPage(IPage page)
	{
		_page = page;
        _lnkLogin = _page.Locator(selector: "text=Login");
        _txtUserName = _page.Locator(selector: "#UserName");
        _txtPassword = _page.Locator(selector: "#Password");
        _btnLogin = _page.Locator(selector: "text=Log in");
        _lnkEmployeeDetails = _page.Locator(selector: "text='Employee Details'");
	}

    public async Task Login(string userName, string password)
    {
        await _txtUserName.FillAsync(userName);
        await _txtPassword.FillAsync(password);
        await _btnLogin.ClickAsync();
    }

    public async Task<bool> IsEmployeeDetailsExists() => await _lnkEmployeeDetails.IsVisibleAsync();
}
