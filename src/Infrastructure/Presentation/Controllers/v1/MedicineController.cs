using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Commands;
using ClinicalBackend.Services.Features.MedicineFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers.v1
{
    public class MedicineController : BaseApiController
    {
        public MedicineController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicine(CreateMedicineCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicineById(int id)
        {
            var result = await _mediator.Send(new GetMedicineQuery { Id = id });
            return result.Match(
                onSuccess: () => Result.Ok(result.Value),
                onFailure: error => BadRequest(error)
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicines()
        {
            var result = await _mediator.Send(new GetAllMedicinesQuery());
            return result.Match(
                onSuccess: () => Result.Ok(result),
                onFailure: error => BadRequest(error)
                );
        }
    }
}
