namespace TimetableHelper.Helpers
{
    public static class NameHelper
    {
        public static Dictionary<int, string> DayName = new Dictionary<int, string>
        {
            { 0, "pondělí"},
            { 1, "úterý"},
            { 2, "středa"},
            { 3, "čtvrtek"},
            { 4, "pátek"}
        };
        public static string GetNameOfDay(int day)
        {
            return DayName[day];
        }
    }
}
