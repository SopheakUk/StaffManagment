using Microsoft.EntityFrameworkCore;
using StaffManagementAPI.Data;
using StaffManagementAPI.Repository.Interface;
using StaffManagementAPI.Service.Interface;
using StaffManagementCore.Exception;
using StaffManagementCore.Model;
using System.Linq.Expressions;

namespace StaffManagementAPI.Service;

public class StaffService(IApplicationDbContext _db) : IStaffService
{
    public async Task<IdResponse> Add(StaffModel model)
    {
        model.EnsureValidForAdd();
        var entity = new Staff(model);
        await _db.Staffs.AddAsync(entity);
        await _db.SaveChangesAsync();

        return new()
        {
            Id= entity.Id,
        };
    }

    public async Task<IdResponse> Update(StaffModel model)
    {
        model.EnsureValidForUpdate();
        var entity = await _db.Staffs.FindAsync(model.Id) ?? throw new ValidatingException("Staff is not found");
        if (await _db.Staffs.AnyAsync(p => p.Id != model.Id && p.StaffId == model.StaffId)) throw new ValidatingException("Cannot update duplicated Staff Id");
        entity.Update(model);
        await _db.SaveChangesAsync();
        return new()
        {
            Id = entity.Id,

        };
    }

    public async Task<IdResponse> Delete(long id)
    {
        var entity = await _db.Staffs.FindAsync(id) ?? throw new ValidatingException("Staff is not found");
        _db.Staffs.Remove(entity);
        await _db.SaveChangesAsync();
        return new()
        {
            Id = entity.Id,
        };
    }

    private static IEnumerable<Expression<Func<Staff, bool>>> Predicate(StaffQueryRequest query)
    {
        if (string.IsNullOrWhiteSpace(query.StaffId) == false)
        {
            Expression<Func<Staff, bool>> predicate = p => p.StaffId == query.StaffId;
            yield return predicate;
        }

        if (query.Gender is Gender gender)
        {
            Expression<Func<Staff, bool>> predicate = p => p.Gender == query.Gender;
            yield return predicate;
        }

        if (query.FromDate is DateTime fromDate)
        {
            Expression<Func<Staff, bool>> predicate = p => p.Birthday >= fromDate;
            yield return predicate;
        }

        if (query.ToDate is DateTime toDate)
        {
            Expression<Func<Staff, bool>> predicate = p => p.Birthday <= toDate;
            yield return predicate;
        }
    }

    public async Task<QueryResponse<StaffModel>> Query(StaffQueryRequest query)
    {
        var results = _db.Staffs.AsNoTracking();
        foreach (var filter in Predicate(query))
        {
            results = results.Where(filter);
        }
        List<StaffModel> items = [];

        foreach (var item in await results.ToArrayAsync())
        {
            var model = item.ToStaffModel();
            items.Add(model);
        }

        return new()
        {
            Results = items,
        };
    }
}