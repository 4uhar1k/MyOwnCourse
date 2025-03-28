using Microsoft.AspNetCore.Mvc;
using MyOwnCourseAPI.Data;
using MyOwnCourseAPI.Domains.Enitites;

namespace MyOwnCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly CourseDBContext _dbContext;
        public RoleController(CourseDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetRoles()
        {
            return _dbContext.Roles;
        }
    }
}
