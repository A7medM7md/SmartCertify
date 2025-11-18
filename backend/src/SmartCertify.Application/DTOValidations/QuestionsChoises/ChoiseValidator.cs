using FluentValidation;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.Interfaces.QuestionsChoice;

namespace SmartCertify.Application.DTOValidations.QuestionsChoices
{
    public class CreateChoiceValidator : AbstractValidator<CreateChoiceDto>
    {
        public CreateChoiceValidator(IQuestionRepository questionRepository)
        {
            RuleFor(c => c.ChoiceText)
                .NotEmpty()
                .WithMessage("Choice text is required.")
                .MaximumLength(200)
                .WithMessage("Choice text cannot exceed 200 characters.");

            RuleFor(c => c.QuestionId)
                .GreaterThan(0)
                .WithMessage("QuestionId must be greater than 0.")
                .MustAsync(async (qId, cancellationToken) => await questionRepository.IsQuestionIdExist(qId))
                .WithMessage("QuestionId does not exist.")
                .WithErrorCode("404");

        }
    }

    public class CreateChoicesValidator : AbstractValidator<IEnumerable<CreateChoiceDto>>
    {
        public CreateChoicesValidator(IQuestionRepository questionRepository)
        {
            RuleForEach(x => x).SetValidator(new CreateChoiceValidator(questionRepository));
        }
    }

    public class UpdateChoiceValidator : AbstractValidator<UpdateChoiceDto>
    {
        public UpdateChoiceValidator()
        {
            RuleFor(c => c.ChoiceText)
                .NotEmpty()
                .WithMessage("Choice text is required.")
                .MaximumLength(200)
                .WithMessage("Choice text cannot exceed 200 characters.");
        }
    }

}
