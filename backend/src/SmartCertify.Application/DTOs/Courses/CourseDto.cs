namespace SmartCertify.Application.DTOs.Courses
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool QuestionsAvailable { get; set; } = false;
        public int QuestionCount { get; set; }
    }
}
