using SmartCertify.Application.DTOs.Questions;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Application.Interfaces.QuestionsChoice
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question?> GetQuestionByIdAsync(int id);
        public Task<bool> IsQuestionIdExist(int id);
        Task<Question> AddQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(Question question);
        Task UpdateQuestionAndChoicesAsync(int id, QuestionDto dto);
    }
}