using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.PatientFeatures.Commands;
using ClinicalBackend.Services.Features.PatientFeatures.PatientQueries;
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

        // Find Patient with input name
        [HttpGet]
        public async Task<IActionResult> GetAllPatient() 
        {
            var result = await _mediator.Send(new GetAllPatientAsync());
            return result.Match(
                onSuccess: () => Result.Ok(result),
                onFailure: error => BadRequest(error)
                );
        }
    }
}
