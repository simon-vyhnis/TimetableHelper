namespace TimetableHelper.Models
{
    public class TimetableDay
    {
        public List<Lesson>[] Lessons { get; set; } = new List<Lesson>[11];
    }
}
