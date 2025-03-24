namespace MyOwnCourseAPI.Domains.Enitites
{
    public record Course
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Category { get; init; }
        public int Creator { get; init; }
        public int Status { get; init; }
    }
}
