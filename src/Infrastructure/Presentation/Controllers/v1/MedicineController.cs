using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Commands;
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
        public async Task<IActionResult> CreateMedicineAsync(CreateMedicineCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMedicineByIdAsync(Guid Id)
        {
            var result = await _mediator.Send(new GetMedicineByIdCommand { Id = Id }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicinesAsync([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var result = await _mediator.Send(new GetAllMedicineCommand { PageNumber = page, PageSize = limit }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditMedicineAsync(Guid Id, [FromBody] EditMedicineCommand command)
        {
            command.Id = Id; // Set the Id from the route parameter
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMedicineAsync(Guid Id)
        {
            var result = await _mediator.Send(new DeleteMedicineCommand { Id = Id }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet("Name/{Name}")]
        public async Task<IActionResult> GetMedicineByNameAsync(string Name, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var result = await _mediator.Send(new GetMedicineByNameCommand { Name = Name, PageNumber = page, PageSize = limit }).ConfigureAwait(false);

            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }
    }
}