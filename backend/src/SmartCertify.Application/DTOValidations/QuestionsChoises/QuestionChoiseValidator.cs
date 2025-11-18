using FluentValidation;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.DTOs.Questions;

namespace SmartCertify.Application.DTOValidations.QuestionsChoices
{
    public class QChoiceValidator : AbstractValidator<ChoiceDto>
    {
        public QChoiceValidator()
        {
            RuleFor(x => x.ChoiceText).NotEmpty().WithMessage("Choice text is required.");
        }
    }

    public class CQuestionValidator : AbstractValidator<QuestionDto>
    {
        public CQuestionValidator()
        {
            RuleFor(x => x.QuestionText).NotEmpty().WithMessage("Question text is required.");
            RuleFor(x => x.DifficultyLevel).NotEmpty().WithMessage("Difficulty level is required.");
        }
    }
}