using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOwnCourseAPI.Domains.Enitites
{
    [Table("users")]
    public record User
    {
        [Key]
        public int Id { get; init; }
        public string Login { get; init; }
        public string Password { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public int Role { get; init; }
    }
}
