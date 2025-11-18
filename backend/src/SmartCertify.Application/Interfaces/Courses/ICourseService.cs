using SmartCertify.Application.DTOs.Courses;

namespace SmartCertify.Application.Interfaces.Courses
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(int courseId);
        Task<bool> IsTitleDuplicateAsync(string title);
        Task AddCourseAsync(CreateCourseDto courseDto);
        Task UpdateCourseAsync(int courseId, UpdateCourseDto courseDto);
        Task DeleteCourseAsync(int courseId);
        Task UpdateDescriptionAsync(int courseId, string description);
    }
}
