using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.PatientFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreatePatient(CreatePatientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // Find all Patients and show it with JSON list
        [HttpGet]
        public async Task<IActionResult> GetAllPatient(int Number, int Size) 
        {
            var result = await _mediator.Send(new GetAllPatientAsync { PageNumber = Number , PageSize = Size}); 
            return Ok(result);
        }

        // Find Patient with Name and show it with JSON list 
        [HttpGet("Name/{Name}")]
        public async Task<IActionResult> GetPatientwithName(string Name)
        {
            var res = await _mediator.Send(new GetPatientByNameAsync { Name = Name });
            return Ok(res);
        }

        // Find patient with Phone number and show it 
        [HttpGet("Phone/{Phone_number}")]
        public async Task<IActionResult> GetPatientbyPhoneNumber(string phoneNumber)
        {
            var res = await _mediator.Send(new FindWithPhoneNumberCommands { Phonenumber = phoneNumber });
            return Ok(res);
        }

        // Update patient infomation 
        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdatePatientDetails(Guid ID, [FromBody] UpdatePatientCommands command)
        {
            command.Id = ID;
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        // Delete patient
        [HttpDelete]
        public async Task<IActionResult> DeletePatient(string phone)
        {
            var res = await _mediator.Send(new DeletePatientWithPhoneNumberCommands { PhoneNumber = phone});
            return Ok(res);
        }

        // Delete patient with ID
        [HttpDelete("ID")]
        public async Task<IActionResult> DeletePatientID(Guid id)
        {
            var res = await _mediator.Send(new DeletePatientWithIDCommand { ID = id});
            return Ok(res);
        }
    }
}
