using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetAllPatientAsync : IRequest<IEnumerable<Patient>>
    {
        // default pagnigation params
        public int Page { get; set; } = 1; 
        public int Limit { get; set; } = 5;  
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

            var pagedPatient = PatientList.Skip((request.Page - 1) * request.Limit) // bỏ qua các phần tử trước đó
                .Take(request.Limit).ToList(); // lấy các phần tử của trang hiện tại
            return pagedPatient.OrderByDescending(p => p.CreatedAt).ToList(); // sort with newest patient depend on createAt
        }
    }
}