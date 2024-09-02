using ClinicalBackend.Services.Features.ReExaminationFeatures.Commands;
using ClinicalBackend.Services.Features.ReExaminations.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers.v1
{
    public class FollowUpController : BaseApiController
    {
        public FollowUpController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFollowUp(CreateFollowUpCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFollowUp()
        {
            var result = await _mediator.Send(new GetAllFollowUpCommand());
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditFollowUp(Guid Id, [FromBody] EditFollowUpCommand command)
        {
            command.Id = Id; // Set the Id from the route parameter
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFollowUp(Guid id)
        {
            var result = await _mediator.Send(new DeleteFollowUpCommand { Id = id });
            return Ok(result);
        }
    }
}