using TimetableHelper.Components.Pages;

namespace TimetableHelper.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int LessonCount { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
        public Class? Class { get; set; }
        public Group? Group { get; set; }
        public List<Room>? SpecialRooms { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Student> Students
        {
            get
            {
                if (Class != null)
                {
                    return Class.Students;
                }
                else if (Group != null)
                {
                    return Group.Students;
                }
                return new List<Student>();
            }
        }
        public string? GetGroupName()
        {
            if(Group != null)
                return Group.Name;
            if(Class != null)
                return Class.Name;
            return null;
        }
    }
}
