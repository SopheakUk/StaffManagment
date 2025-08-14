using NSubstitute;
using StaffManagementAPI.Repository.Interface;
using StaffManagementAPI.Service;
using StaffManagementAPI.Service.Interface;
using StaffManagementCore.Exception;
using StaffManagementCore.Model;

namespace UnitTest;

internal class StaffServiceTest
{
    private IStaffService _staffService;

    [SetUp]
    public void Setup()
    {
        var db = Substitute.For<IApplicationDbContext>();
        _staffService = new StaffService(db);
    }

    [Test]
    public void CannotAddIfStaffIdIsEmpty()
    {
        var staffModel = new StaffModel
        {
            Birthday = DateTime.Today,
            Gender = Gender.Male,
            FullName = "Hello World"
        };
        var ex = Assert.ThrowsAsync<ValidatingException>(async () => await _staffService.Add(staffModel));
        Assert.That(ex.Message, Is.EqualTo("Staff Id is required"));
    }

    [Test]
    public void CannotAddIfGenderIsNotDefine()
    {
        var staffModel = new StaffModel
        {
            Birthday = DateTime.Today,
            Gender = (Gender)3,
            FullName = "Hello World",
            StaffId = "01234567899",
        };
        var ex = Assert.ThrowsAsync<ValidatingException>(async () => await _staffService.Add(staffModel));
        Assert.That(ex.Message, Is.EqualTo("Invalid Gender"));
    }

    [Test]
    public void CannotAddIfStaffIdMoreThan10()
    {
        var staffModel = new StaffModel
        {
            StaffId = "01234567899",
            Birthday = DateTime.Today,
            Gender = Gender.Male,
            FullName = "Hello World"
        };
        var ex = Assert.ThrowsAsync<ValidatingException>(async () => await _staffService.Add(staffModel));
        Assert.That(ex.Message, Is.EqualTo("Staff Id is limited 10 characters"));
    }

    [Test]
    public void CannotAddIfFullNameIsEmpty()
    {
        var staffModel = new StaffModel
        {
            StaffId = "123",
            Birthday = DateTime.Today,
            Gender = Gender.Male,
            FullName = ""
        };
        var ex = Assert.ThrowsAsync<ValidatingException>(async () => await _staffService.Add(staffModel));
        Assert.That(ex.Message, Is.EqualTo("Full Name is required"));
    }


    [Test]
    public void CannotAddIfFullNameMoreThan100()
    {
        var staffModel = new StaffModel
        {
            StaffId = "01234",
            Birthday = DateTime.Today,
            Gender = Gender.Male,
            FullName = "Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World"
        };
        var ex = Assert.ThrowsAsync<ValidatingException>(async () => await _staffService.Add(staffModel));
        Assert.That(ex.Message, Is.EqualTo("Full Name is limited 100 characters"));
    }

    [Test]
    public void CannotUpdateIfStaffIdIsEmpty()
    {
        var staffModel = new StaffModel
        {
            Birthday = DateTime.Today,
            Gender = Gender.Male,
            FullName = "Hello World"
        };
        var ex = Assert.ThrowsAsync<ValidatingException>(async () => await _staffService.Update(staffModel));
        Assert.That(ex.Message, Is.EqualTo("Staff Id is required"));
    }

    [Test]
    public void CannotUpdateIfStaffIdMoreThan10()
    {
        var staffModel = new StaffModel
        {
            StaffId = "01234567899",
            Birthday = DateTime.Today,
            Gender = Gender.Male,
            FullName = "Hello World"
        };
        var ex = Assert.ThrowsAsync<ValidatingException>(async () => await _staffService.Update(staffModel));
        Assert.That(ex.Message, Is.EqualTo("Staff Id is limited 10 characters"));
    }

    [Test]
    public void CannotUpdateIfFullNameIsEmpty()
    {
        var staffModel = new StaffModel
        {
            StaffId = "123",
            Birthday = DateTime.Today,
            Gender = Gender.Male,
            FullName = ""
        };
        var ex = Assert.ThrowsAsync<ValidatingException>(async () => await _staffService.Update(staffModel));
        Assert.That(ex.Message, Is.EqualTo("Full Name is required"));
    }


    [Test]
    public void CannotUpdateIfFullNameMoreThan100()
    {
        var staffModel = new StaffModel
        {
            StaffId = "01234",
            Birthday = DateTime.Today,
            Gender = Gender.Male,
            FullName = "Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World"
        };
        var ex = Assert.ThrowsAsync<ValidatingException>(async () => await _staffService.Update(staffModel));
        Assert.That(ex.Message, Is.EqualTo("Full Name is limited 100 characters"));
    }
}