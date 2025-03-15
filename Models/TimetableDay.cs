using TimetableHelper.Helpers;

namespace TimetableHelper.Models
{
    public class TimetableDay
    {
        public string Name { get; set; }
        public int Number {  get; set; }
        public List<Lesson>[] Lessons { get; set; } = new List<Lesson>[11];

        public TimetableDay(int number)
        {
            Number = number;
            Name = NameHelper.GetNameOfDay(number);
            for (int i = 0; i < Lessons.Count(); i++)
            {
                Lessons[i] = new List<Lesson>();
            }
        }
    }
}
