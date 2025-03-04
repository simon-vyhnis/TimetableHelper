namespace TimetableHelper.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; } = new List<Student>();
        public Class? Class { get; set; }
        public int? ClassId { get; set; }
        public List<Subject> Subjects { get; } = new List<Subject>();
    }
}
