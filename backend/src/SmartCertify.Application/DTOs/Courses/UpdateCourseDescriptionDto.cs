using System.ComponentModel.DataAnnotations;

namespace SmartCertify.Application.DTOs.Courses
{
    public class UpdateCourseDescriptionDto
    {
        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
