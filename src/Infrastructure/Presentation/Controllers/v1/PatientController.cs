using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.PatientFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers.v1
{
    public class PatientController : BaseApiController
    {
        public PatientController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
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
            var result = await _mediator.Send(new GetAllPatientAsync { Page = page, Limit = limit }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        // Find patient with Phone number and show it
        [HttpGet("Phone/{Phone}")]
        public async Task<IActionResult> GetPatientbyPhoneNumberAsync(string Phone)
        {
            var res = await _mediator.Send(new FindWithPhoneNumberCommands { PhoneNumber = Phone }).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error)); 
        }

        // Update patient infomation
        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdatePatientDetailsAsync(Guid ID, [FromBody] UpdatePatientCommands command)
        {
            command.Id = ID;
            var res = await _mediator.Send(command).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error)); 
        }

        [HttpDelete("ID")]
        public async Task<IActionResult> DeletePatientIDAsync(Guid ID)
        {
            var res = await _mediator.Send(new DeletePatientWithIDCommand { ID = ID }).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error));         
        }
    }
}