using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.Interfaces.QuestionsChoises;

namespace SmartCertify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoicesController : ControllerBase
    {
        private readonly IChoiceService _service;
        private readonly IValidator<CreateChoiceDto> _createChoiceValidator;
        private readonly IValidator<IEnumerable<CreateChoiceDto>> _createChoicesValidator;

        public ChoicesController(IChoiceService service,
            IValidator<CreateChoiceDto> createChoiceValidator,
            IValidator<IEnumerable<CreateChoiceDto>> createChoicesValidator)
        {
            _service = service;
            _createChoiceValidator = createChoiceValidator;
            _createChoicesValidator = createChoicesValidator;
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<IEnumerable<ChoiceDto>>> GetChoices(int questionId)
        {
            return Ok(await _service.GetAllChoicesAsync(questionId));
        }

        [HttpGet("{questionId}/{id}")]
        public async Task<ActionResult<ChoiceDto>> GetChoice(int questionId, int id)
        {
            var choice = await _service.GetChoiceByIdAsync(id);
            return choice == null ? NotFound() : Ok(choice);
        }

        [HttpPost("single")]
        public async Task<IActionResult> CreateChoice([FromBody] CreateChoiceDto dto)
        {
            var validationResult = await _createChoiceValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _service.AddChoiceAsync(dto);
            return Created(); //CreatedAtAction(nameof(GetChoices), new { questionId = dto.QuestionId });
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreateChoices([FromBody] IEnumerable<CreateChoiceDto> dtos)
        {
            var validationResult = await _createChoicesValidator.ValidateAsync(dtos);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _service.AddChoicesAsync(dtos);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChoice(int id, [FromBody] UpdateChoiceDto dto)
        {
            await _service.UpdateChoiceAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserChoice(int id, [FromBody] UpdateUserChoice dto)
        {
            await _service.UpdateUserChoiceAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChoice(int id)
        {
            await _service.DeleteChoiceAsync(id);
            return NoContent();
        }
    }

}