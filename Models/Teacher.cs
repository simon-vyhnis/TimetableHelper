namespace TimetableHelper.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TargetHours { get; set; }
        public List<Subject> Subjects { get; } = new List<Subject>();
        public string GetInitials()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return string.Empty;

            return string.Join("", Name.Split(' ', '-', '.')
                                       .Where(w => !string.IsNullOrWhiteSpace(w))
                                       .Select(w => char.ToUpper(w[0])));
        }
    }
}
