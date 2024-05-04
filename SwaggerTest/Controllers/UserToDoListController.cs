using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerTest.Interface;
using SwaggerTest.Models;

namespace SwaggerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserToDoListController : Controller
    {
      private readonly IUserToDo userToDo;
       public UserToDoListController(IUserToDo _userToDo)
        {
            userToDo = _userToDo;
        }

        [HttpPost("getalltask")]
        [AllowAnonymous]
        public async Task<List<UserToDoModelVM>> GetAllTask(int userid)
        {
            try
            {
                var result = await userToDo.GetAllTask(userid);
                if (result != null)
                {
                    return result;
                }
                else return null;
            }
            catch { throw; }
        }

        [HttpPost("gettaskbyid")]
        [AllowAnonymous]
        public async Task<IEnumerable<UserToDoModelVM>> GetTaskById(int id,int userid)
        {
            try
            {
                var data = await userToDo.GetTaskByID(id,userid);
                if (data != null)
                {
                    return data;

                }
                else return null;
            }
            catch { throw; }
        }
        [HttpPost("addtask")]
        [AllowAnonymous]
        public async Task<int> AddTask([FromBody]UserToDoModelIM user)
        {
            try
            {
                var data = await userToDo.AddNewTask(user);
                if (data == 1)
                {
                    return 1;

                }
                else return 0;
            }
            catch { throw; }
        }
        [HttpPost("updatetask")]
        [AllowAnonymous]
        public async Task<int> UpdateTask([FromBody]UserToDoModelIM user)
        {
            try
            {
                var data = await userToDo.UpdateTask(user);
                if (data == 1)
                {
                    return 1;

                }
                else return 0;
            }
            catch { throw; }
        }
        [HttpPost("deletetask")]
        [AllowAnonymous]
        public async Task<int> DeleteTask(int id, int userid)
        {
            try
            {
                var data = await userToDo.DeleteTask(id,userid);
                if (data == 1)
                {
                    return 1;

                }
                else return 0;
            }
            catch { throw; }
        }
        [HttpPost("markasdone")]
        [AllowAnonymous]
        public async Task<int> MarkasDone(int id, int userid)
        {
            try
            {
                var data = await userToDo.MarkAsDoneTask(id,userid);
                if (data == 1)
                {
                    return 1;

                }
                else return 0;
            }
            catch { throw; }
        }
        


    }
}
