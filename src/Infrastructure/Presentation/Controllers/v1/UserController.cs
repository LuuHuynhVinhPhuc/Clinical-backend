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
            return Ok(await _mediator.Send(command));
        }
    }
}
