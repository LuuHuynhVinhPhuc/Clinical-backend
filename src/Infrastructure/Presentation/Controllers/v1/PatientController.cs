using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.PatientFeatures.Commands;
using ClinicalBackend.Services.Features.PatientFeatures.PatientFind;
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

        // Find Patient with input name
        [HttpGet("{name}")]
        public async Task<IActionResult> FindPatientByName(string name)
        {
            var res = await _mediator.Send(new FindPatientWithName { Name = name });

            // return 
            return res.Match(
                    onSuccess: () => Result.Ok(res.Value),
                    onFailure: error => BadRequest(error)
                );
        }

    }
}
