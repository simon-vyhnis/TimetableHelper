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
        public int GetLessonCount()
        {
            var result = 0;
            foreach(var subject in Subjects)
            {
                result += subject.LessonCount;
            }
            return result;
        }
        public string GetGroupNames()
        {
            var result = "";
            foreach (var subject in Subjects)
            {
                if(!result.Contains(subject.GetGroupName()))
                {
                    result += subject.GetGroupName() + ", ";
                }
            }
            if(result.Length > 2)
            {
                return result[..^2];
            }
            return result;
        }
    }
}
