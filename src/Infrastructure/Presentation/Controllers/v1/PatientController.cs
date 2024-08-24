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
            return result.Match(
                    onSuccess: () => Result.Ok(result.Value),
                    onFailure: error => BadRequest(error)
                );
        }

        // Find all Patients and show it with JSON list
        [HttpGet]
        public async Task<IActionResult> GetAllPatient() 
        {
            var result = await _mediator.Send(new GetAllPatientAsync());
            return Ok(result);
        }

        // Find Patient with Name and show it with JSON list 
        [HttpGet("{Name}")]
        public async Task<IActionResult> GetPatientwithName(string Name)
        {
            var res = await _mediator.Send(new GetPatientByNameAsync { Name = Name} );
            return Ok(res);
        }

        // Edit Patient and re-update it 
        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdatePatient( string Patient )
        {
            var res = await _mediator.Send(new UpdatePatientCommands { PatientName = Patient });
            return Ok(res);
        }

        // Delete Patient and re-update it 
        [HttpDelete("{Name}")]
        public async Task<IActionResult> DeletePatient(string name)
        {
            var res = await _mediator.Send(new DeletePatientCommands { Name = name });
            return Ok(res);
        }
    }
}
