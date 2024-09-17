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
            return Ok(result);
        }

        // Find all Patients and show it with JSON list
        [HttpGet]
        public async Task<IActionResult> GetAllPatientAsync([FromQuery] int Number = 1, [FromQuery] int Size = 5)
        {
            var result = await _mediator.Send(new GetAllPatientAsync { Page = Number, Limit = Size }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        // Find patient with Phone number and show it
        [HttpGet("Phone")]
        public async Task<IActionResult> GetPatientbyPhoneNumberAsync([FromQuery] string phoneNumber)
        {
            var res = await _mediator.Send(new FindWithPhoneNumberCommands { Phonenumber = phoneNumber }).ConfigureAwait(false);
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
            return Ok(res);
        }

        // Delete patient
        //[HttpDelete]
        //public async Task<IActionResult> DeletePatientAsync(string phone)
        //{
        //    var res = await _mediator.Send(new DeletePatientWithPhoneNumberCommands { PhoneNumber = phone }).ConfigureAwait(false);
        //    return Ok(res);
        //}

        // Delete patient with ID
        [HttpDelete("ID")]
        public async Task<IActionResult> DeletePatientIDAsync(Guid id)
        {
            var res = await _mediator.Send(new DeletePatientWithIDCommand { ID = id }).ConfigureAwait(false);
            return Ok(res);
        }
    }
}