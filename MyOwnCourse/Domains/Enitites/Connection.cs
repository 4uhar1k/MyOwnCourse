namespace MyOwnCourseAPI.Domains.Enitites
{
    public record Connection
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int CourseId { get; init; }
        public int Type { get; init; }
    }
}
