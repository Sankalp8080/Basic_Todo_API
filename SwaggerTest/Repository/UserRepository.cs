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


        public async Task<IEnumerable<UserVM>> GetUserInfo(int id)
        {
            var param = new SqlParameter("@slno", id);
            try
            {
                return await Task.Run(() => _dbContext.userVMs.FromSqlRaw(@"exec GetUserInfoById @slno", param).ToListAsync());
            }
            catch { throw; }
        }

        public async Task<int> AddUpdateUserInfo(UserIM userIM)
        {
            var param = new List<SqlParameter>();
            var uniquekey = userIM.uniquekey == null ? Guid.NewGuid() : userIM.uniquekey;
            var slnoch = userIM.slno == 0 ? 0 : userIM.slno;
            param.Add(new SqlParameter("@slno", slnoch));
            param.Add(new SqlParameter("@username", userIM.username));
            param.Add(new SqlParameter("@firstname", userIM.firstname));
            param.Add(new SqlParameter("@lastname", userIM.lastname));
            param.Add(new SqlParameter("@email", userIM.email));
            param.Add(new SqlParameter("@uniquekey", uniquekey));
            param.Add(new SqlParameter("@isActive", userIM.isActive));
            param.Add(new SqlParameter("@password", userIM.password));
            try
            {
                var res = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"exec AddUpdateUser @slno,@username,@firstname,@lastname,@email,@uniquekey,@isActive,@password", param.ToArray()));
                return res;
            }
            catch { throw; }
        }

        public async Task<int> DeleteUserInfo(int slno)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@slno", slno));
            try
            {
                return await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"exec DeleteUser @slno", param));
            }
            catch { throw; }
        }
        public async Task<int> StoreToken(string username,string token)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@username", username),
                new SqlParameter("@token", token)
            };
            try
            {
                return await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"StoreToken @username,@token ", param));
            }
            catch { throw; }
        }
        public async Task<List<UserVM>> CheckLogin(string username, string password)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@username", username));
            param.Add(new SqlParameter("@password", password));
            try
            {
                return await  _dbContext.userVMs.FromSqlRaw<UserVM>(@"CheckLogin @username,@password ", param.ToArray()).ToListAsync();
            }
            catch { throw; }
        }
    }
}
