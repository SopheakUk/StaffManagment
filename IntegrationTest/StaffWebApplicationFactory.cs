using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StaffManagementAPI.Repository;
using StaffManagementAPI.Repository.Interface;

namespace IntegrationTest;

public class StaffWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            var connectionString = "server=127.0.0.1;uid=root;database=staffmanagement_test";
            var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseMySql(connectionString, serverVersion, opt =>
            {
                opt.UseMicrosoftJson();
            }));
        });
    }
}