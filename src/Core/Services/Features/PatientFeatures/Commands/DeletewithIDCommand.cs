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
    public class DeletewithIDCommand :  IRequest<Result<DeletewithIDResponse>>
    {
        public Guid ID { get; set; }
    }

    public class DeletewithIDResponse
    {
        public string response { get; set; }
    }

    // task
    public class DeletewithIDHandler : IRequestHandler<DeletewithIDCommand, Result<DeletewithIDResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletewithIDHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<DeletewithIDResponse>> Handle(DeletewithIDCommand request, CancellationToken cancellationToken)
        {
            // find all patient
            var patient = await _unitOfWork.Patient.GetByIdAsync(request.ID);
            if (patient == null)
                return Result.Failure<DeletewithIDResponse>(PatientError.NotFoundID(request.ID));

            // remove the exitsting
            _unitOfWork.Patient.Remove(patient);
            // Save changes to the repository
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var res = new DeletewithIDResponse() { response = "Patient deleted successfully"};

            return Result.Success(res);
        }
    }
}
