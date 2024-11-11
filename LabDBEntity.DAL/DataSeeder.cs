using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabDBEntity.DAL
{
    public class DataSeeder
    {
        private readonly LabDbContext _context;

        public DataSeeder(LabDbContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            if (_context.Students.Any() || _context.Courses.Any() || _context.Teachers.Any())
            {
                return;
            }

            var teachers = new List<Teacher>
        {
            new Teacher { FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" },
            new Teacher { FirstName = "Jane", LastName = "Smith", Email = "janesmith@example.com" },
        };

            _context.Teachers.AddRange(teachers);

            var courses = new List<Course>
        {
            new Course { Title = "Mathematics", Credits = 5, Description = "An introduction to Mathematics." },
            new Course { Title = "Physics", Credits = 4, Description = "Fundamentals of Physics." },
        };

            _context.Courses.AddRange(courses);

            var students = new List<Student>
        {
            new Student { FirstName = "Michael", LastName = "Johnson", Email = "michaeljohnson@example.com" },
            new Student { FirstName = "Sarah", LastName = "Williams", Email = "sarahwilliams@example.com" },
        };

            _context.Students.AddRange(students);

            await _context.SaveChangesAsync();

            var studentCourses = new List<StudentCourse>
        {
            new StudentCourse { StudentId = students[0].StudentId, CourseId = courses[0].CourseId },
            new StudentCourse { StudentId = students[1].StudentId, CourseId = courses[1].CourseId },
        };

            _context.StudentCourses.AddRange(studentCourses);

            var teacherCourses = new List<TeacherCourse>
        {
            new TeacherCourse { TeacherId = teachers[0].TeacherId, CourseId = courses[0].CourseId },
            new TeacherCourse { TeacherId = teachers[1].TeacherId, CourseId = courses[1].CourseId },
        };

            _context.TeacherCourses.AddRange(teacherCourses);

            await _context.SaveChangesAsync();
        }
    }
}
