using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator _mediator;

        protected BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
