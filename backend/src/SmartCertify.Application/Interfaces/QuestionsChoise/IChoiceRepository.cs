using SmartCertify.Domain.Entities;

namespace SmartCertify.Application.Interfaces.QuestionsChoises
{
    public interface IChoiceRepository
    {
        Task<IEnumerable<Choice>> GetAllChoicesAsync(int questionId);
        Task<Choice?> GetChoiceByIdAsync(int id);
        Task AddChoiceAsync(Choice choice);
        Task AddChoicesAsync(IEnumerable<Choice> choices);
        Task UpdateChoiceAsync(Choice choice);
        Task DeleteChoiceAsync(Choice choice);
    }
}