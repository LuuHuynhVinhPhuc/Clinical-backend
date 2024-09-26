using AutoMapper;
using MediatR;

namespace ClinicalBackend.Presentation.Controllers.v1
{
    public class PrescriptionController : BaseApiController
    {
        public PrescriptionController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}