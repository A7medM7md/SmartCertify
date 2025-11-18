using AutoMapper;
using SmartCertify.Application.DTOs.Courses;
using SmartCertify.Application.Interfaces.Courses;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Application.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task AddCourseAsync(CreateCourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            course.CreatedBy = 2; // Replace with actual user context [When Implementing Auth]
            course.CreatedOn = DateTime.UtcNow;

            await _courseRepository.AddCourseAsync(course);
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course is null) throw new KeyNotFoundException($"Course with id {courseId} not found.");

            await _courseRepository.DeleteCourseAsync(course);
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();

            var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return courseDtos;
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            var courseDto = course is null ? null : _mapper.Map<CourseDto>(course);
            return courseDto;
        }

        public async Task<bool> IsTitleDuplicateAsync(string title)
        {
            return await _courseRepository.IsTitleDuplicateAsync(title);
        }

        public async Task UpdateCourseAsync(int courseId, UpdateCourseDto courseDto)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course is null) throw new KeyNotFoundException("Course not found.");

            _mapper.Map(courseDto, course);
            await _courseRepository.UpdateCourseAsync(course);
        }

        public async Task UpdateDescriptionAsync(int courseId, string description)
        {
            await _courseRepository.UpdateDescriptionAsync(courseId, description);
        }
    }
}
