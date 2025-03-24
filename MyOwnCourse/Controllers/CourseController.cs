//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MyOwnCourseAPI.Data;
using MyOwnCourseAPI.Domains.Enitites;

namespace MyOwnCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController: ControllerBase
    {
        private readonly CourseDBContext _courseDBContext;
        public CourseController(CourseDBContext courseDBContext)
        {
            _courseDBContext = courseDBContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return _courseDBContext.Courses;
        }
        [HttpGet("id/{Id}")]
        public async Task<ActionResult<Course?>> GetCourseById(int Id)
        {
            var FoundCourse = _courseDBContext.Courses.Where(n => n.Id == Id).SingleOrDefault();
            if (FoundCourse == null)
            {
                return NotFound();
            }
            else
                return FoundCourse;

        }
        [HttpGet("category/{Category}")]
        public IEnumerable<Course> GetCoursesByCategory(string category)
        {
            var listofcourses = _courseDBContext.Courses.Where(n => n.Category == category);
            if (listofcourses!=null)
                return listofcourses;
            return null;
        }
        [HttpPost]
        public async Task<ActionResult> CreateCourse(Course course)
        {
            await _courseDBContext.Courses.AddAsync(course);
            await _courseDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCourse(Course course)
        {
            _courseDBContext.Courses.Update(course);
            await _courseDBContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCourse(int Id)
        {
            var CourseToDelete = await GetCourseById(Id);
            if (CourseToDelete.Value is null)
            {
                return NotFound();
            }
            _courseDBContext.Courses.Remove(CourseToDelete.Value);
            await _courseDBContext.SaveChangesAsync();
            return Ok();
        }
    }
}
