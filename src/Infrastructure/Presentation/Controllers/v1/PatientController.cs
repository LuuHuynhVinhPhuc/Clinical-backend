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
        public async Task<IActionResult> GetAllPatientAsync(int Number, int Size)
        {
            var result = await _mediator.Send(new GetAllPatientAsync { Page = Number, Limit = Size }).ConfigureAwait(false);
            return Ok(result);
        }

        // Find Patient with Name and show it with JSON list
        [HttpGet("Name/{Name}")]
        public async Task<IActionResult> GetPatientwithNameAsync(string Name)
        {
            var res = await _mediator.Send(new GetPatientByNameAsync { Name = Name }).ConfigureAwait(false);
            return Ok(res);
        }

        // Find patient with Phone number and show it
        [HttpGet("Phone/{Phone_number}")]
        public async Task<IActionResult> GetPatientbyPhoneNumberAsync(string phoneNumber)
        {
            var res = await _mediator.Send(new FindWithPhoneNumberCommands { Phonenumber = phoneNumber }).ConfigureAwait(false);
            return Ok(res);
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
        [HttpDelete]
        public async Task<IActionResult> DeletePatientAsync(string phone)
        {
            var res = await _mediator.Send(new DeletePatientWithPhoneNumberCommands { PhoneNumber = phone }).ConfigureAwait(false);
            return Ok(res);
        }

        // Delete patient with ID
        [HttpDelete("ID")]
        public async Task<IActionResult> DeletePatientIDAsync(Guid id)
        {
            var res = await _mediator.Send(new DeletePatientWithIDCommand { ID = id }).ConfigureAwait(false);
            return Ok(res);
        }
    }
}