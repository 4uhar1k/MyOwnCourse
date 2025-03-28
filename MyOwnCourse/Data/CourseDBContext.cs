using Microsoft.EntityFrameworkCore;
using MyOwnCourseAPI.Domains.Enitites;

namespace MyOwnCourseAPI.Data
{
    public class CourseDBContext: DbContext
    {
        public CourseDBContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
