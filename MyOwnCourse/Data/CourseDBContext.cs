using Microsoft.EntityFrameworkCore;
using MyOwnCourse.Domains.Enitites;

namespace MyOwnCourse.Data
{
    public class CourseDBContext: DbContext
    {
        public CourseDBContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
