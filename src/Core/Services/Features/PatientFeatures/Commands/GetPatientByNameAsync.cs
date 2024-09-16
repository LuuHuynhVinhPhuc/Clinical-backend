using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByNameAsync : IRequest<List<Patient>>
    {
        public string Name { get; set; }
    }

    // Task
    public class GetPatientBIDHandler : IRequestHandler<GetPatientByNameAsync, List<Patient>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPatientBIDHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Patient>> Handle(GetPatientByNameAsync request, CancellationToken cancellationToken)
        {
            // Get Name and compare it 
            var FindPatient = await _unitOfWork.Patient.FindWithNameAsync(request.Name).ConfigureAwait(false);
            // return value ( check null first )
            return FindPatient ?? new List<Patient> { };
        }
    }
}