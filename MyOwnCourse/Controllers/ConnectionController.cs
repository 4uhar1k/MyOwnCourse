using Microsoft.AspNetCore.Mvc;
using MyOwnCourseAPI.Data;
using MyOwnCourseAPI.Domains.Enitites;

namespace MyOwnCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController: ControllerBase
    {
        private readonly CourseDBContext _dbContext;

        public ConnectionController(CourseDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Connection>> GetConnections()
        {
            return _dbContext.Connections;
        }
        [HttpGet("courseid/{CourseId}")]
        public IEnumerable<Connection> GetConnectionsByCourseId (int CourseId)
        {
            var listofconnections = _dbContext.Connections.Where(n => n.CourseId == CourseId);
            if (listofconnections != null)
            {
                return listofconnections;
            }
            return null;
        }
        

        [HttpPost]
        public async Task<ActionResult> CreateConnection (Connection connection)
        {
            await _dbContext.Connections.AddAsync(connection);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConnectionsByCourseId), new { id = connection.CourseId }, connection);
        }
    }
    
}
