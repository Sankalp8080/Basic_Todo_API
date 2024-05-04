using SwaggerTest.Models;

namespace SwaggerTest.Interface
{
    public interface IUserToDo
    {
        public Task<List<UserToDoModelVM>> GetAllTask(int id);
        public Task<IEnumerable<UserToDoModelVM>> GetTaskByID(int id, int userid);
        public Task<int> AddNewTask(UserToDoModelIM userim);
        public Task<int>UpdateTask(UserToDoModelIM userToDoModelIM);
        public Task<int> DeleteTask(int id, int userid);
        public Task<int>MarkAsDoneTask(int id, int userid);

    }
}
