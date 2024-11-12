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

        [HttpGet("Specialty/{Specialty}")]
        public async Task<IActionResult> GetMedicineBySpecialtyAsync(string Specialty, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var result = await _mediator.Send(new GetMedicineBySpecialtyCommand { Specialty = Specialty, PageNumber = page, PageSize = limit }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet("Details/Keyword={Word}")]
        public async Task<IActionResult> GetMedicineDetailsAsync(string Word)
        {
            var result = await _mediator.Send(new GetMedicineDetailsCommand { Word = Word }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }
        // Get medicines by date
        [HttpGet("Date/Start={startDate}&End={endDate}")]
        public async Task<IActionResult> GetMedicinesByDateAsync(string startDate, string endDate, [FromQuery] int page = 1, [FromQuery] int limit = 5)
        {
            var res = await _mediator.Send(new GetMedicinesbyDateCommand { StartDate = startDate, EndDate = endDate, Page = page, Limit = limit }).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet("Prescription/Range/Start={startDate}&End={endDate}")]
        public async Task<IActionResult> GetMedicineByStatusAsync(string startDate, string endDate, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var res = await _mediator.Send(new GetMedicineByRangeCommand { StartDate = startDate, EndDate = endDate, PageNumber = page, PageSize = limit }).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet("Prescription/Range/Top10/Start={startDate}&End={endDate}")]
        public async Task<IActionResult> GetTop10MedicineAsync(string startDate, string endDate)
        {
            var res = await _mediator.Send(new GetMedicineByTop10Command { StartDate = startDate, EndDate = endDate}).ConfigureAwait(false);
            return res.Match(
                onSuccess: () => Result.Ok(res.Value()),
                onFailure: error => Result.BadRequest(error));
        }
    }
}