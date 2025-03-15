using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TimetableHelper.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                var day = new TimetableDay(i);
                for (int j = 0; j < 10; j++)
                {
                    var lessons = await context.Lesson
                        .Where(l => l.Day == i && l.Number == j && (l.Subject.Class.Id == clas || l.Subject.Group.Class.Id == clas || l.Subject.Group.Students.Any(s => s.ClassId == clas)))
                        .Include(l => l.Subject)
                        .Include(l => l.Subject.Teacher)
                        .Include(l => l.Subject.Group)
                        .Include(l => l.Subject.Class)
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
                var day = new TimetableDay(i);
                for (int j = 0; j < 10; j++)
                {
                    var lesson = await context.Lesson
                        .Where(l => l.Day == i && l.Number == j && l.Room.Id == room)
                        .Include(l => l.Subject)
                        .Include(l => l.Subject.Teacher)
                        .Include(l => l.Subject.Group)
                        .Include(l => l.Subject.Class)
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
                var day = new TimetableDay(i);
                for (int j = 0; j < 10; j++)
                {
                    var lesson = await context.Lesson
                        .Where(l => l.Day == i && l.Number == j && l.Subject.TeacherId == teacher)
                        .Include(l => l.Subject)
                        .Include(l => l.Subject.Teacher)
                        .Include(l => l.Subject.Group)
                        .Include(l => l.Subject.Class)
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

        public async Task<List<Lesson>> GetCurrentLessons(int day, int number, int? clas, int? teacher, int? room)
        {
            using var context = _dbFactory.CreateDbContext();
            if (clas.HasValue)
            {
                return await context.Lesson
                    .Where(l => l.Day == day && l.Number == number &&
                     (l.Subject.Class.Id == clas || l.Subject.Group.Class.Id == clas || l.Subject.Group.Students.Any(s => s.ClassId == clas)))
                    .Include(l => l.Subject.Students)
                    .Include(l => l.Subject.Teacher)
                    .Include(l => l.Subject.Group)
                    .Include(l => l.Subject.Class)
                    .ToListAsync();
            }
            else if (teacher.HasValue)
            {
                return await context.Lesson
                    .Where(l => l.Day == day && l.Number == number && l.Subject.TeacherId == teacher)
                    .ToListAsync();
            }
            else
            {
                return await context.Lesson
                    .Where(l => l.Day == day && l.Number == number && l.Room.Id == room)
                    .ToListAsync();
            }
        }

        public async Task<List<Lesson>> GetAvailableLessons(int day, int number, int? clas, int? teacher, int? room)
        {
            var result = new List<Lesson>();
            using var context = _dbFactory.CreateDbContext();
            if (clas.HasValue)
            {
                var subjects = await context.Subject
                    .Where(s => (s.Class.Id == clas || s.Group.Students.Any(s => s.Class.Id == clas)))
                    .Where(s => s.LessonCount > s.Lessons.Count())
                    .Include(s => s.Students)
                    .Include(s => s.Teacher)
                    .Include(s => s.Group)
                    .Include(s => s.Class)
                    .ToListAsync();
                foreach (var subject in subjects)
                {
                    bool collides = false;
                    //check if every student is available 
                    foreach (var student in subject.Students)
                    {
                        if(await context.Lesson.Where(l => l.Day == day && l.Number == number && l.Subject.Students.Contains(student)).AnyAsync())
                        {
                            collides = true;
                            break;
                        }
                    }
                    if (collides)
                        continue;
                    //check if any room is available
                    if (!(await GetAvailableRooms(day, number, subject.Students.Count())).Any())
                    {
                        collides = true;
                        continue;
                    }
                    //check if the teacher is available
                    if(!await IsTeacherAvailable(day, number, subject.Teacher))
                    {
                        collides = true;
                    }
                    if (!collides)
                    {
                        var newLesson = new Lesson();
                        newLesson.Subject = subject;
                        newLesson.Number = number;
                        newLesson.Day = day;
                        result.Add(newLesson);
                    }
                }
            }
            else if (teacher.HasValue)
            {
                return await context.Lesson
                    .Where(l => l.Day == day && l.Number == number && l.Subject.TeacherId == teacher)
                    .ToListAsync();
            }
            else
            {
                return await context.Lesson
                    .Where(l => l.Day == day && l.Number == number && l.Room.Id == room)
                    .ToListAsync();
            }
            return result;
        }

        public async Task<List<Room>> GetAvailableRooms(int day, int number, int capacity)
        {
            using var context = _dbFactory.CreateDbContext();
            var rooms = await context.Room.Where(r => r.Capacity >= capacity).ToListAsync();
            var lessons = await context.Lesson.Where(l => l.Day == day && l.Number == number).ToListAsync();
            var result = new List<Room>();
            foreach (var room in rooms)
            {
                if(!lessons.FindAll(l => l.Room.Id == room.Id).Any())
                    result.Add(room);
            }
            return result;
        }

        public async Task<bool> IsTeacherAvailable(int day, int number, Teacher teacher)
        {
            using var context = _dbFactory.CreateDbContext();
            return !await context.Lesson
                .Where(l => l.Day == day && l.Number == number && l.Subject.TeacherId == teacher.Id)
                .AnyAsync();
        }

        public async Task AddLesson(Lesson lesson)
        {
            using var context = _dbFactory.CreateDbContext();
            lesson.Room = context.Room.Find(lesson.Room.Id);
            lesson.Subject = context.Subject.Find(lesson.Subject.Id);
            //lesson.Subject.Teacher = context.Teacher.Find(lesson.Subject.Teacher.Id);
            await context.Lesson.AddAsync(lesson);
            await context.SaveChangesAsync();
        }

        public async Task DeleteLesson(Lesson lesson)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Lesson.Remove(lesson);
            await context.SaveChangesAsync();
        }
    }
}
