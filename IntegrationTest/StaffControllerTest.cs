using StaffManagementCore.Model;
using System.Net.Http.Json;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace IntegrationTest;

[TestCaseOrderer("IntegrationTest.AlphabeticalOrderer", "IntegrationTest")]
public class StaffControllerTest
{
    private static readonly StaffModel _model = new()
    {
        Birthday = DateTime.Today,
        FullName = "Sopheak",
        Gender = Gender.Male,
        StaffId = Random.Shared.NextInt64().ToString("D7")[..7]
    };

    [Fact]
    public async Task _1_Add()
    {
        var application = new StaffWebApplicationFactory();

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/api/staff", _model);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IdResponse>();
        Assert.Equal(ResponseCodeEnum.Ok, result.ResponseCode);

        _model.Id = result.Id;
    }

    [Fact]
    public async Task _2_Update()
    {
        var application = new StaffWebApplicationFactory();
        _model.FullName = "Thida";
        _model.Gender = Gender.Female;

        var client = application.CreateClient();

        var response = await client.PutAsJsonAsync("/api/staff", _model);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IdResponse>();
        Assert.Equal(ResponseCodeEnum.Ok, result.ResponseCode);
    }

    [Fact]
    public async Task _3_Delete()
    {
        var application = new StaffWebApplicationFactory();

        var client = application.CreateClient();

        var response = await client.DeleteAsync($"/api/staff/{_model.Id}");

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IdResponse>();
        Assert.Equal(ResponseCodeEnum.Ok, result.ResponseCode);
    }

    [Fact]
    public async Task _4_Query()
    {
        var application = new StaffWebApplicationFactory();

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/api/staff/query", new StaffQueryRequest());

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<QueryResponse<StaffModel>>();
        Assert.Equal(ResponseCodeEnum.Ok, result.ResponseCode);
    }
}

public class AlphabeticalOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        return testCases.OrderBy(tc => tc.TestMethod.Method.Name);
    }
}