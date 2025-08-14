using Microsoft.EntityFrameworkCore;
using StaffManagementCore.Model;
using System.ComponentModel.DataAnnotations;

namespace StaffManagementAPI.Data;

[Index(nameof(StaffId), IsUnique = true)]
public class Staff : EntityBase
{
    public Staff()
    {
    }

    public Staff(StaffModel model)
    {
        Set(model);
    }

    public StaffModel ToStaffModel() => new()
    {
        StaffId = StaffId,
        FullName = FullName,
        Birthday = Birthday,
        Gender = Gender,
        Id = Id,
    };

    internal void Update(StaffModel model)
    {
        Set(model);
    }

    private void Set(StaffModel model)
    {
        StaffId = model.StaffId;
        FullName = model.FullName;
        Birthday = model.Birthday ?? throw new ValidationException("Invalid Birthday");
        Gender = model.Gender;
    }

    [MaxLength(8)]
    public string StaffId { get; set; }

    [MaxLength(100)]
    public string FullName { get; set; }

    public DateTime Birthday { get; set; }
    public Gender Gender { get; set; }
}