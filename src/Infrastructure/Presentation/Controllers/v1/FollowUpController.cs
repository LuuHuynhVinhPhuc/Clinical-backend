using AutoMapper;
using ClinicalBackend.Contracts.DTOs;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.FollowUpFeatures.Commands;
using ClinicalBackend.Services.Features.FollowUpsFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers.v1
{
    public class FollowUpController : BaseApiController
    {
        public FollowUpController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFollowUpAsync(CreateFollowUpCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFollowUpAsync([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var result = await _mediator.Send(new GetAllFollowUpCommand { PageNumber = page, PageSize = limit }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetFollowUpByIdAsync([FromRoute] Guid Id)
        {
            var result = await _mediator.Send(new GetFollowUpByIdCommand { PatientId = Id }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditFollowUpAsync(Guid Id, [FromBody] EditFollowUpCommand command)
        {
            command.Id = Id; // Set the Id from the route parameter
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFollowUpAsync(Guid Id)
        {
            var result = await _mediator.Send(new DeleteFollowUpCommand { Id = Id }).ConfigureAwait(false);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }
    }
}