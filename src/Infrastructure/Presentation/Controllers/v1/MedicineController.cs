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
        public async Task<IActionResult> CreateMedicine(CreateMedicineCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMedicineById(Guid id)
        {
            var result = await _mediator.Send(new GetMedicineByIdCommand { Id = id });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicines([FromQuery] int page = 1, [FromQuery] int limit = 100)
        {
            var result = await _mediator.Send(new GetAllMedicineCommand { PageNumber = page, PageSize = limit });
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditMedicine(Guid Id, [FromBody] EditMedicineCommand command)
        {
            command.Id = Id; // Set the Id from the route parameter
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMedicine(Guid id)
        {
            var result = await _mediator.Send(new DeleteMedicineCommand { Id = id });
            return Ok(result);
        }

        [HttpGet("Name/{Name}")]
        public async Task<IActionResult> GetMedicineByName(string Name, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var result = await _mediator.Send(new GetMedicineByNameCommand { Name = Name, PageNumber = page, PageSize = limit });
            return Ok(result);
        }
    }
}