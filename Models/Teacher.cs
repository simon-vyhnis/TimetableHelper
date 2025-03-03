namespace TimetableHelper.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TargetHours { get; set; }
        public List<Subject> Subjects { get; } = new List<Subject>();
    }
}
