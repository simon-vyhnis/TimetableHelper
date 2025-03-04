using System.Text.RegularExpressions;

namespace TimetableHelper.Models
{
    public class Lesson
    {
        public Group Group { get; set; }
        public Teacher Teacher { get; set; }
    }
}
