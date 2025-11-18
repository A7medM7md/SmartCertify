using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartCertify.Application.DTOs.Courses;
using SmartCertify.Application.Interfaces.Courses;

namespace SmartCertify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IValidator<CreateCourseDto> _createCourseValidator;
        private readonly IValidator<UpdateCourseDto> _updateCourseValidator;

        public CoursesController(ICourseService courseService,
            IValidator<CreateCourseDto> createCourseValidator,
            IValidator<UpdateCourseDto> updateCourseValidator)
        {
            _courseService = courseService;
            _createCourseValidator = createCourseValidator;
            _updateCourseValidator = updateCourseValidator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }


        /// <summary>
        /// Retrieves a specific course by ID.
        /// </summary>
        /// <param name="id">The ID of the course to retrieve.</param>
        /// <returns>The course with the specified ID.</returns>
        /// <response code="200">Returns the course if found.</response>
        /// <response code="404">If the course is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            return course == null ? NotFound() : Ok(course);
        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        /// <param name="createCourseDto">The details of the course to create.</param>
        /// <returns>The newly created course.</returns>
        /// <response code="201">Returns the created course.</response>
        /// <response code="400">If the input is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreateCourseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto createCourseDto)
        {
            var validationResult = await _createCourseValidator.ValidateAsync(createCourseDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _courseService.AddCourseAsync(createCourseDto);
            //return CreatedAtAction(nameof(GetCourse), new { id = createCourseDto.Title }, createCourseDto);
            return CreatedAtAction(null, null, createCourseDto);
        }

        /// <summary>
        /// Updates an existing course.
        /// </summary>
        /// <param name="id">The ID of the course to update.</param>
        /// <param name="updateCourseDto">The updated course details.</param>
        /// <response code="204">Indicates the update was successful.</response>
        /// <response code="400">If the input is invalid.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto updateCourseDto)
        {
            var validationResult = await _updateCourseValidator.ValidateAsync(updateCourseDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _courseService.UpdateCourseAsync(id, updateCourseDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a course.
        /// </summary>
        /// <param name="id">The ID of the course to delete.</param>
        /// <response code="204">Indicates the deletion was successful.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Updates the description of a course.
        /// </summary>
        /// <param name="id">The ID of the course to update.</param>
        /// <param name="model">The updated course description.</param>
        /// <response code="204">Indicates the update was successful.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateDescription([FromRoute] int id,
            [FromBody] UpdateCourseDescriptionDto model)
        {
            await _courseService.UpdateDescriptionAsync(id, model.Description);
            return NoContent();
        }
    }




}
