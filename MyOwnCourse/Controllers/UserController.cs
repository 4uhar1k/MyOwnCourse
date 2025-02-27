using Microsoft.AspNetCore.Mvc;
using MyOwnCourseAPI.Data;
using MyOwnCourseAPI.Domains.Enitites;

namespace MyOwnCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly CourseDBContext _courseDBContext;
        public UserController(CourseDBContext courseDBContext)
        {
            _courseDBContext = courseDBContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _courseDBContext.Users;
        }
        [HttpGet("id/{Id}")]
        public async Task<ActionResult<User?>> GetUserById(int Id)
        {
            return _courseDBContext.Users.Where(n => n.Id == Id).SingleOrDefault();
        }
        [HttpGet("login/{Login}/password/{Password}")]
        public async Task<ActionResult<User?>> GetUserByLoginNPass(string Login, string Password)
        {
            return _courseDBContext.Users.Where(n => n.Login == Login && n.Password == Password).SingleOrDefault();
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            await _courseDBContext.Users.AddAsync(user);
            await _courseDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new {id = user.Id}, user);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(User user)
        {
            _courseDBContext.Users.Update(user);
            await _courseDBContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            var UserToDelete = await GetUserById(Id);
            if (UserToDelete.Value is null)
            {
                return NotFound();
            }
            _courseDBContext.Users.Remove(UserToDelete.Value);
            await _courseDBContext.SaveChangesAsync();
            return Ok();
        }
    }
}
