using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetAllPatientAsync : IRequest<IEnumerable<Patient>>
    {
    }

    // Task
    public class GetAllPatientHandler : IRequestHandler<GetAllPatientAsync, IEnumerable<Patient>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPatientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Patient>> Handle(GetAllPatientAsync request, CancellationToken cancellationToken)
        {
            var PatientList = await _unitOfWork.Patient.GetAllAsync().ConfigureAwait(false);
            return PatientList.OrderByDescending(p => p.CreatedAt).ToList(); // sort with newest patient depend on createAt
        }
    }
}