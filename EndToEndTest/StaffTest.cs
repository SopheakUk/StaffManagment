using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace EndToEndTest;

public class StaffTest : PageTest
{
    private const string _url = "http://localhost:5217/";

    [Fact]
    public async Task HasTitle()
    {
        await Page.GotoAsync(_url);
        await Expect(Page).ToHaveTitleAsync(new Regex("Staff Management"));
    }

    [Fact]
    public async Task HasAddNew()
    {
        await Page.GotoAsync(_url);
        var getStarted = Page.GetByRole(AriaRole.Button, new() { Name = "Add New" });
        await getStarted.ClickAsync();
        var addStaff = Page.GetByText("Add Staff");

        await Expect(addStaff).ToBeVisibleAsync();
    }

    [Fact]
    public async Task AddNewStaff()
    {
        await Page.GotoAsync(_url);

        var addStaff = Page.GetByRole(AriaRole.Button, new() { Name = "Add New" });
        await addStaff.ClickAsync();

        var addStaffForm = Page.GetByTestId("addStaffForm");
        await Expect(addStaffForm).ToBeVisibleAsync();

        await Page.GetByTestId("staffId").FillAsync(Random.Shared.Next().ToString("D7")[..7]);
        await Page.GetByTestId("fullName").FillAsync("End to end test");
        await Page.GetByTestId("birthDay").ClickAsync();
        var calendar = Page.GetByText(DateTime.Today.ToString("MMMM yyyy"));
        await Expect(calendar).ToBeVisibleAsync();
        await Page.GetByRole(AriaRole.Button, new()
        {
            Name = "14"
        }).ClickAsync();

        await Expect(calendar).ToBeHiddenAsync();

        await Page.GetByTestId("buttonAdd").ClickAsync();

        await Expect(addStaffForm).ToBeHiddenAsync();

        await Page.ScreenshotAsync(new()
        {
            Path = "screenshot.png"
        });
    }
}