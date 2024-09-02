using System.Runtime.CompilerServices;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.ReExaminationsFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers.v1
{
    public class ReExaminationController : BaseApiController
    {
        public ReExaminationController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReExamination(CreateReExaminationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{PatientId}")]
        public async Task<IActionResult> GetReExaminationByPatientId(Guid PatientId)
        {
            var result = await _mediator.Send(new GetReExaminationCommand { PatientId = PatientId });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReExaminations()
        {
            var result = await _mediator.Send(new GetAllReExaminationCommand());
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditReExamination(Guid Id, [FromBody] EditReExaminationCommand command)
        {
            command.Id = Id; // Set the Id from the route parameter
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteReExamination(Guid id)
        {
            var result = await _mediator.Send(new DeleteReExaminationCommand{ Id = id });
            return Ok(result);
        }
    }
}