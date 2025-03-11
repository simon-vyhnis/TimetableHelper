using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TimetableHelper.Models;

namespace TimetableHelper.Data
{
    public class TableData
    {
        private IDbContextFactory<TimetableHelperContext> _dbFactory;
        public TableData(IDbContextFactory<TimetableHelperContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
   
        public async Task<TimetableDay[]?> GetTimetable(int? clas, int? room, int? teacher)
        {
            if (clas != null)
                return await GetClassTimetable((int)clas);
            if (room != null)
                return await GetRoomTimetable((int)room);
            if (teacher != null)
                return await GetClassTimetable((int)teacher);
            return null;
        }

        public async Task<TimetableDay[]> GetClassTimetable(int clas)
        {
            var result = new TimetableDay[5];
            using var context = _dbFactory.CreateDbContext();
            for (int i = 0; i < 5; i++)
            {
                var day = new TimetableDay();
                for (int j = 0; j < 10; j++)
                {
                    var lessons = await context.Lesson
                        .Where(l => l.Day == i && l.Number == j && (l.Subject.Class.Id == clas || l.Subject.Group.Class.Id == clas || l.Subject.Group.Students.Any(s => s.ClassId == clas)))
                        .ToListAsync();
                    day.Lessons[j] = lessons;
                }
                result[i] = day;
            }
            return result;
        }

        public async Task<TimetableDay[]> GetRoomTimetable(int room)
        {
            var result = new TimetableDay[5];
            using var context = _dbFactory.CreateDbContext();
            for (int i = 0; i < 5; i++)
            {
                var day = new TimetableDay();
                for (int j = 0; j < 10; j++)
                {
                    var lesson = await context.Lesson
                        .Where(l => l.Day == i && l.Number == j && l.Room.Id == room)
                        .FirstOrDefaultAsync();
                    if (lesson != null)
                    {
                        day.Lessons[j].Add(lesson);
                    }
                }
                result[i] = day;
            }
            return result;
        }

        public async Task<TimetableDay[]> GetTeacherTimetable(int teacher)
        {
            var result = new TimetableDay[5];
            using var context = _dbFactory.CreateDbContext();
            for (int i = 0; i < 5; i++)
            {
                var day = new TimetableDay();
                for (int j = 0; j < 10; j++)
                {
                    var lesson = await context.Lesson
                        .Where(l => l.Day == i && l.Number == j && l.Subject.TeacherId == teacher)
                        .FirstOrDefaultAsync();
                    if (lesson != null)
                    {
                        day.Lessons[j].Add(lesson);
                    }
                }
                result[i] = day;
            }
            return result;
        }
    }
}
