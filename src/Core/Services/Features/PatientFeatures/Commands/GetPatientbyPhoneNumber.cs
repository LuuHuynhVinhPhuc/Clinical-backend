using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using ClinicalBackend.Services.Features.MedicineFeatures;
using Domain.Interfaces;
using MediatR;
using MapsterMapper;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientbyPhoneNumber : IRequest<Result<FindWithPhoneReponse>>
    {
        public string PhoneNumber { get; set; }
    }

    public class FindWithPhoneReponse
    {
        public List<Patient> Patients { get; set; }
    }
    // Task
    public class FindWithPhoneNumberHandler : IRequestHandler<GetPatientbyPhoneNumber, Result<FindWithPhoneReponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FindWithPhoneNumberHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<FindWithPhoneReponse>> Handle(GetPatientbyPhoneNumber request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patient.FindWithPhoneNumberAsync(request.PhoneNumber).ConfigureAwait(false);

            // check exist
            if (patient == null)
                return Result.Failure<FindWithPhoneReponse>(PatientError.PhoneNotFound(request.PhoneNumber.ToString()));

            var res = new FindWithPhoneReponse { Patients = new List<Patient> { patient } };
            return Result.Success(res);
        }
    }
}