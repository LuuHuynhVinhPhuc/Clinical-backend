using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalBackend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator _mediator;
        protected IMapper _mapper;


        protected BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected BaseApiController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        protected BaseApiController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}