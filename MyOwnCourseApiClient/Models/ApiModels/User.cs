﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyOwnCourseApiClient.Models.ApiModels
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Role { get; set; }
    }
}
