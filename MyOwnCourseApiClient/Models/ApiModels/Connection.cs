using System;
using System.Collections.Generic;
using System.Text;

namespace MyOwnCourseApiClient.Models.ApiModels
{
    public class Connection
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int Type { get; set; }
    }
}
