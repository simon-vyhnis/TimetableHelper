namespace TimetableHelper.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public Subject Subject { get; set; }
        public Room Room { get; set; }
        public int Day {  get; set; } 
        public int Number { get; set; }        
    }
}
