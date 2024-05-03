using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SwaggerTest.Data;
using SwaggerTest.Interface;
using SwaggerTest.Models;

namespace SwaggerTest.Repository
{
    public class UserRepository : IUser
    {
        private readonly DatabaseDBContext _dbContext;
        public UserRepository(DatabaseDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserVM>> GetUserList()
        {
            try
            {
                return await _dbContext.userVMs.FromSqlRaw<UserVM>("GetUserList").ToListAsync();
            }
            catch { throw; }
        }
        //public UserVM GetUser(int slno)
        //{
        //    try
        //    {
        //        UserVM? user = _dbContext.userVMs.Find(slno);
        //        if (user != null)
        //        {
        //            return user;
        //        }
        //        else
        //        {
        //            throw new ArgumentException();
        //        }

        //    }
        //    catch { throw; }
        //}

        public async Task<IEnumerable<UserVM>> GetUserInfo(int id)
        {
            var param = new SqlParameter("@slno", id);
            try
            {
                return await Task.Run(() => _dbContext.userVMs.FromSqlRaw("GetUserInfoById", param).ToListAsync());
            }
            catch { throw; }
        }

        public async Task<int> AddUpdateUserInfo(UserIM userIM)
        {
            var param = new List<SqlParameter>();
            var uniquekey = userIM.uniquekey == null ? Guid.NewGuid() : userIM.uniquekey;

            param.Add(new SqlParameter("@slno", userIM.slno));
            param.Add(new SqlParameter("@username", userIM.username));
            param.Add(new SqlParameter("@firstname", userIM.firstname));
            param.Add(new SqlParameter("@lastname", userIM.lastname));
            param.Add(new SqlParameter("@email", userIM.email));
            param.Add(new SqlParameter("@uniquekey", uniquekey));
            param.Add(new SqlParameter("@isActive", userIM.isActive));
            param.Add(new SqlParameter("@password", userIM.password));
            try
            {
                return await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync("AddUpdateUser", param.ToArray()));
            }
            catch { throw; }
        }

        public async Task<int> DeleteUserInfo(int slno)
        {
            try {
                return await Task.Run(() =>_dbContext.Database.ExecuteSqlRawAsync("DeleteUser",slno));
            }
            catch { throw; }
        }
    }
}
