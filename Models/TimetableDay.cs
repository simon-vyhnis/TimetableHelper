using TimetableHelper.Helpers;

namespace TimetableHelper.Models
{
    public class TimetableDay
    {
        public string Name { get; set; }
        public List<Lesson>[] Lessons { get; set; } = new List<Lesson>[11];

        public TimetableDay(int number)
        {
            Name = NameHelper.GetNameOfDay(number);
        }
    }
}
