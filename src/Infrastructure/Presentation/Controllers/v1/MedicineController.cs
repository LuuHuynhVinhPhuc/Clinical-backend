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
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{Name}")]
        public async Task<IActionResult> GetMedicineById(string Name)
        {
            var result = await _mediator.Send(new GetMedicineCommand { Name = Name }).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicines()
        {
            var result = await _mediator.Send(new GetAllMedicineCommand()).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditMedicine(Guid Id, [FromBody] EditMedicineCommand command)
        {
            command.Id = Id; // Set the Id from the route parameter
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMedicine(Guid id)
        {
            var result = await _mediator.Send(new DeleteMedicineCommand { Id = id }).ConfigureAwait(false);
            return Ok(result);
        }
    }
}