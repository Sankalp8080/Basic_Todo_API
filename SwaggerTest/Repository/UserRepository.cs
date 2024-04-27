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

        public List<UserVM> GetUserList()
        {
            try
            {
                return _dbContext.userVMs.ToList();
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

        public UserVM GetUser(int id)
        {
            try
            {
                return _dbContext.userVMs.FromSqlRaw("GetUserInfoById",id).FirstOrDefault();
            }
            catch { throw; }
        }
    }
}
