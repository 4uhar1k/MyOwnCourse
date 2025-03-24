using System;
using System.Collections.Generic;
using System.Text;

namespace MyOwnCourseApiClient.Models.ApiModels
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Creator { get; set; }
        public int Status { get; set; }
    }
}
