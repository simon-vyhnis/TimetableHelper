﻿namespace TimetableHelper.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public List<Student> Students { get; } = new List<Student>();
    }
}
