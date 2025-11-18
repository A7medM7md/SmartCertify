using Microsoft.EntityFrameworkCore;
using SmartCertify.Application.Interfaces.Courses;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SmartCertifyDbContext _dbContext;

        public CourseRepository(SmartCertifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var courses = await _dbContext.Courses.ToListAsync();
            return courses;
        }

        public async Task<Course?> GetCourseByIdAsync(int courseId)
        {
            return await _dbContext.Courses.FindAsync(courseId);
        }

        public async Task<bool> IsTitleDuplicateAsync(string title)
        {
            return await _dbContext.Courses.AnyAsync(c => c.Title == title);
        }

        public async Task AddCourseAsync(Course? course)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course? course)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            _dbContext.Courses.Update(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(Course? course)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateDescriptionAsync(int courseId, string description)
        {
            var course = await _dbContext.Courses.FindAsync(courseId);
            if (course == null) throw new KeyNotFoundException("Course not found.");

            course.Description = description;
            await _dbContext.SaveChangesAsync();
        }

    }
}

/*
Key Concepts:
async :

Marks a method as asynchronous.
Allows the use of the await keyword inside the method.
Must return a Task, Task<T>, or void (for event handlers).

await:

Waits for an asynchronous task to complete.
Pauses the execution of the method until the awaited task finishes without blocking the main thread.
*/