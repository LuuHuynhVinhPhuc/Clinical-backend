using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using ClinicalBackend.Services.Features.MedicineFeatures;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class FindWithPhoneNumberCommands : IRequest<Result<FindWithPhoneReponse>>
    {
        public string PhoneNumber { get; set; }
    }

    public class FindWithPhoneReponse
    {
        public List<Patient> Patient { get; set; }
    }
    // Task
    public class FindWithPhoneNumberHandler : IRequestHandler<FindWithPhoneNumberCommands, Result<FindWithPhoneReponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FindWithPhoneNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<FindWithPhoneReponse>> Handle(FindWithPhoneNumberCommands request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patient.FindWithPhoneNumberAsync(request.PhoneNumber).ConfigureAwait(false);

            // check exist
            if (patient == null)
                return Result.Failure<FindWithPhoneReponse>(PatientError.NotFoundPhone(request.PhoneNumber.ToString()));

            var res = new FindWithPhoneReponse { Patient = new List<Patient> { patient } };
            return Result.Success(res);
        }
    }
}