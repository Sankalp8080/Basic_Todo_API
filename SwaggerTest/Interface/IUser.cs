using SwaggerTest.Models;

namespace SwaggerTest.Interface
{
    public interface IUser
    {
        public Task <List<UserVM>> GetUserList();
        public Task<IEnumerable<UserVM>> GetUserInfo(int id);
        public Task<int> StoreToken(string username, string token);
        public Task<int> AddUpdateUserInfo(UserIM userIM);
        public Task<int> DeleteUserInfo(int id);
        public Task<List<UserVM>> CheckLogin(string username,string password);
    }
}
