namespace TimetableHelper.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public List<Student> Students { get; } = new List<Student>();
        public List<Group> Groups { get; } = new List<Group>();
        public List<Subject> Subjects { get; } = new List<Subject>();
    }
}
