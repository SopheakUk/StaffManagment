using MudBlazor.Services;
using StaffManagement.Components;
using StaffManagement.Service;
using StaffManagement.Service.Interface;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

var uri = builder.Configuration["uri"];
builder.Services.AddScoped(p =>
{
    var httpClient = new HttpClient { BaseAddress = new(uri), };
    httpClient.DefaultRequestHeaders.Accept.Add(new("*/*"));
    httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MyHttpClient/1.0");
    return httpClient;
});

builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IStaffService, StaffService>();

builder.Services.AddSyncfusionBlazor();
SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF5cXmRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXdfcnZXR2hYUUZ/VkJWYEk=");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
