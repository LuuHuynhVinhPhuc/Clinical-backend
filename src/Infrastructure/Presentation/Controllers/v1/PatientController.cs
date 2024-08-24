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
        [HttpPost("Create_Patient")]
        public async Task<IActionResult> CreatePatient(CreatePatientCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                    onSuccess: () => Result.Ok(result.Value),
                    onFailure: error => BadRequest(error)
                );
        }

        // Find all Patients and show it with JSON list
        [HttpGet("Get_all_patients")]
        public async Task<IActionResult> GetAllPatient() 
        {
            var result = await _mediator.Send(new GetAllPatientAsync());
            return Ok(result);
        }

        // Find Patient with Name and show it with JSON list 
        [HttpGet("{Name}/FindPatientWithName")]
        public async Task<IActionResult> GetPatientwithName(string Name)
        {
            var res = await _mediator.Send(new GetPatientByNameAsync { Name = Name });
            return Ok(res);
        }

        // Find patient with Phone number and show it 
        [HttpGet("{Phone_number}/FindPatientWithPhoneNumber")]
        public async Task<IActionResult> GetPatientbyPhoneNumber(string phoneNumber)
        {
            var res = await _mediator.Send(new FindWithPhoneNumberCommands { Phonenumber = phoneNumber });
            return Ok(res);
        }

        // Update patient infomation 
        [HttpPut("{Phone}/UpdatePatientDetails")]
        public async Task<IActionResult> UpdatePatientDetails(string name)
        {
            var res = await _mediator.Send(new UpdatePatientCommands { PatientName = name});
            return Ok(res);
        }

        // Delete patient
        [HttpDelete("Delete_Patient")]
        public async Task<IActionResult> DeletePatient(string phone)
        {
            var res = await _mediator.Send(new DeletePatientWithPhoneNumberCommands { PhoneNumber = phone});
            return Ok(res);
        }
    
    }
}
