using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SwaggerTest.Data;
using SwaggerTest.Interface;
using SwaggerTest.Models;

namespace SwaggerTest.Repository
{
    public class UserToDoRepository:IUserToDo
    {
        private readonly DatabaseDBContext _dbContext;
        public UserToDoRepository(DatabaseDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<UserToDoModelVM>> GetAllTask(int id)
        { 
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@userid", id));
            try
            {
                return await _dbContext.userToDoModelsvm.FromSqlRaw<UserToDoModelVM>(@"exec GetAllTask @userid",param.ToArray()).ToListAsync();
            }
            catch { throw; }
        }
        public async Task<IEnumerable<UserToDoModelVM>> GetTaskByID(int id,int userid)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@slno", id));
            param.Add(new SqlParameter("@userid", userid));
            
            try
            {
                return await Task.Run(() => _dbContext.userToDoModelsvm.FromSqlRaw(@"exec GetTaskById  @slno={0}, @userid={1}", id, userid).ToListAsync());
            }
            catch { throw; }
        }
        public async Task<int> AddNewTask(UserToDoModelIM userim)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@userid", userim.userId));
            param.Add(new SqlParameter("@task", userim.task));
            param.Add(new SqlParameter("@remdate", userim.remainderDate));
            param.Add(new SqlParameter("@taskp", userim.taskpriority));
            try
            {
                var res = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"exec AddTask @userid,@task,@remdate,@taskp", param.ToArray()));
                return res;
            }
            catch { throw; }
        }
        public async Task<int> UpdateTask(UserToDoModelIM userim)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@userid", userim.userId));
            param.Add(new SqlParameter("@task", userim.task));
            param.Add(new SqlParameter("@remdate", userim.remainderDate));
            param.Add(new SqlParameter("@taskp", userim.taskpriority));
            param.Add(new SqlParameter("@slno", userim.slno));
            try
            {
                var res = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"exec UpdateTask @userid,@task,@remdate,@taskp,@slno", param.ToArray()));
                return res;
            }
            catch { throw; }
        }
        public async Task<int> DeleteTask(int id, int userid)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@slno", id));
            param.Add(new SqlParameter("@userid", userid));

            try
            {
                return await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"exec DeleteTask @slno,@userid", param));
            }
            catch { throw; }
        }
        public async Task<int> MarkAsDoneTask(int id, int userid)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@slno", id));
            param.Add(new SqlParameter("@userid", userid));

            try
            {
                return await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"exec MarkAsDone @slno,@userid", param));
            }
            catch { throw; }
        }
    }
}
