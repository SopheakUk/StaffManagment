using Microsoft.EntityFrameworkCore;
using StaffManagementAPI.Data;
using System.Data;
using System.Data.Common;

namespace StaffManagementAPI.Repository.Interface;

public interface IApplicationDbContext
{
    DbSet<Staff> Staffs { get; set; }

    Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead);

    Task CloseConnectionAsync();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
