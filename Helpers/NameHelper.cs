namespace TimetableHelper.Helpers
{
    public static class NameHelper
    {
        public static Dictionary<int, string> DayName = new Dictionary<int, string>
        {
            { 0, "po"},
            { 1, "út"},
            { 2, "st"},
            { 3, "čt"},
            { 4, "pá"}
        };
        public static string GetNameOfDay(int day)
        {
            return DayName[day];
        }
    }
}
