using FluentValidation;
using SmartCertify.Application.DTOs.Courses;
using SmartCertify.Application.Interfaces.Courses;

namespace SmartCertify.Application.DTOValidations.Courses
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseDto>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseValidator(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            ApplyValidations();
            ApplyCustomValidations();
        }

        public void ApplyValidations()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        }

        public void ApplyCustomValidations()
        {
            RuleFor(x => x.Title).MustAsync(async (title, cancellationToken) => title == null || !await _courseRepository.IsTitleDuplicateAsync(title))
                .WithMessage("The course title should be unique. The title you passed does exists.");
        }
    }
}
