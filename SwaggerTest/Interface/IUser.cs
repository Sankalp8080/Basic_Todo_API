using SwaggerTest.Models;

namespace SwaggerTest.Interface
{
    public interface IUser
    {
        public List<UserVM> GetUserList();
        public UserVM GetUserInfo(int id);
        public void UpdateUserInfo(UserVM userVM);
        public void AddUserInfo(UserVM userVM);
        public UserVM DeleteUserInfo(int id);
    }
}
