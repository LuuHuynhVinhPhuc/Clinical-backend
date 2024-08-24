using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class DeletePatientWithPhoneNumberCommands : IRequest<Result<DeletePatientResponse>>
    {
        public string PhoneNumber { get; set; }
    }

    public class DeletePatientResponse
    {
        public string Response { get; set; }
    }
    
    // task
    public class DeletePatientWithPhoneNumberHandler : IRequestHandler<DeletePatientWithPhoneNumberCommands, Result<DeletePatientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePatientWithPhoneNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DeletePatientResponse>> Handle(DeletePatientWithPhoneNumberCommands request, CancellationToken cancellationToken)
        {
            // find all patient details with Phone number 
            var patient = await _unitOfWork.PatientInfo.FindWithPhoneNumberAsync(request.PhoneNumber);
            // check exits
            if (patient == null)
            {
                return Result.Failure<DeletePatientResponse>(PatientError.NotFoundPhone(request.PhoneNumber));
            }

            // remove patient 
            foreach (var items in patient.ToList())
            {
                _unitOfWork.PatientInfo.Remove(items);
            };
            // save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            // return value 
            return Result.Success(new DeletePatientResponse { Response = "Patient deleted successfully" });
        }
    }
}
