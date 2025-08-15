using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StaffManagementAPI.Data;
using StaffManagementAPI.Repository.Interface;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace StaffManagementAPI.Repository;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public async Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead)
    {
        var transaction = await Database.BeginTransactionAsync(isolationLevel);
        return transaction.GetDbTransaction();
    }

    public async Task CloseConnectionAsync() => await Database.CloseConnectionAsync();

    public DbSet<Staff> Staffs { get; set; }
}