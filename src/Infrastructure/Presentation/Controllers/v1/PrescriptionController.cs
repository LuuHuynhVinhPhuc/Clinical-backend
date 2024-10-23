using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.PrescriptionFeatures.Commands;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers.v1
{
    public class PrescriptionController : BaseApiController
    {
        public PrescriptionController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescriptionAsync(CreatePrescriptionCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrescriptionAsync([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var result = await _mediator.Send(new GetAllPrescriptionCommand { PageNumber = page, PageSize = limit }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPrescriptionByIdAsync(Guid Id)
        {
            var result = await _mediator.Send(new GetPrescriptionByIdCommand { Id = Id }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet("Phone/{Phone}")]
        public async Task<IActionResult> GetPrescriptionByPhoneAsync(string Phone, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var result = await _mediator.Send(new GetPrescriptionByPhoneCommand { PhoneNumber = Phone, PageNumber = page, PageSize = limit }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditPrescriptionAsync(Guid Id, [FromBody] EditPrescriptionCommand command)
        {
            command.Id = Id; // Set the Id from the route parameter
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePrescriptionAsync(Guid Id)
        {
            var result = await _mediator.Send(new DeletePrescriptionCommand { Id = Id }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }
    }
}