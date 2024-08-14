using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.UserFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers.v1
{
    public class UserController : BaseApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                onSuccess: () => Result.Ok(result.Value()),
                onFailure: error => Result.BadRequest(error));
        }
    }
}
