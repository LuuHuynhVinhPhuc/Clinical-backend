using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.PatientFeatures.Commands;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers.v1
{
    public class PatientController : BaseApiController
    {
        public PatientController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // Create with Entity elements
        [HttpPost]
        public async Task<IActionResult> CreatePatientAsync(CreatePatientCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        // Find all Patients and show it with JSON list
        [HttpGet]
        public async Task<IActionResult> GetAllPatientAsync([FromQuery] int page = 1, [FromQuery] int limit = 5)
        {
            var result = await _mediator.Send(new GetAllPatientCommands { Page = page, Limit = limit }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        // Find patient with ID
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPatientbyIDAsync(Guid Id)
        {
            var result = await _mediator.Send(new GetPatientByIDCommand { ID = Id }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        // Find patient with Phone number and show it
        [HttpGet("Phone/{Phone}")]
        public async Task<IActionResult> GetPatientbyPhoneNumberAsync(string Phone)
        {
            var res = await _mediator.Send(new GetPatientbyPhoneNumber { PhoneNumber = Phone }).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        // Get patient by date
        [HttpGet("Date/{dateStart}&{dateEnd}")]
        public async Task<IActionResult> GetPatientByDateAsync(string dateStart, string dateEnd, [FromQuery] int page = 1, [FromQuery] int limit = 5)
        {
            var res = await _mediator.Send(new GetPatientByDateCommand { DateStart = dateStart, DateEnd = dateEnd, Page = page, Limit = limit }).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        // Update patient infomation
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePatientDetailsAsync(Guid Id, [FromBody] UpdatePatientCommands command)
        {
            command.Id = Id;
            var res = await _mediator.Send(command).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePatientbyIDAsync(Guid Id)
        {
            var res = await _mediator.Send(new DeletePatientbyIDCommand { ID = Id }).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error));
        }
    }
}