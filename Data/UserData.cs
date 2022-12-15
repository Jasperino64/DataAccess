using DataAccess.DbAccess;
using DataAccess.Models;
using System.Runtime.CompilerServices;

namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;
    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>(
            storedProcedure: "dbo.spUsers_GetAll",
            new { });

    public async Task<UserModel?> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>(
            storedProcedure: "dbo.spUsers_Get",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task InsertUser(UserModel user) =>
        _db.SaveData(
            storedProcedure: "dbo.spUsers_Insert",
            new { user.FirstName, user.LastName, user.Email });

    public Task UpdateUser(UserModel user) =>
        _db.SaveData(storedProcedure: "dbo.spUsers_Update",
            user);

    public Task DeleteUser(int id) =>
        _db.SaveData(storedProcedure: "dbo.spUsers_Delete", new { Id = id });
}
