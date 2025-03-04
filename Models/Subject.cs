namespace TimetableHelper.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int LessonCount {  get; set; }
        public Teacher Teacher { get; set; }
        public Class? Class { get; set; }
        public Group? Group { get; set; }
        public List<Room>? SpecialRooms { get; set; }
    }
}
