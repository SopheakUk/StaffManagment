using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StaffManagementAPI.Filter;
using StaffManagementAPI.Repository;
using StaffManagementAPI.Repository.Interface;
using StaffManagementAPI.Service;
using StaffManagementAPI.Service.Interface;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(configure =>
        {
            configure.Filters.Add<ExceptionFilter>();
        });
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(p =>
        {
            p.UseInlineDefinitionsForEnums();
            p.CustomSchemaIds(type => type.FullName.Replace("+", "."));
            p.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API",
                Version = "v1",
                Description = "API Document"
            });
        });
        builder.Services.AddSwaggerGenNewtonsoftSupport();
        builder.Services.AddScoped<IStaffService, StaffService>();


        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
        builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseMySql(connectionString, serverVersion, opt =>
            {
                opt.UseMicrosoftJson();
            }));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        var app = builder.Build();

        //var scopedServices = builder.Services.Where(s => s.Lifetime == ServiceLifetime.Scoped).ToList();

        //foreach (var service in scopedServices)
        //{
        //    Console.WriteLine($"Scoped: {service.ServiceType.FullName}");
        //}


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {

        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}