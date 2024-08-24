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
    public class DeletePatientCommands : IRequest<Result<DeletePatientRepsonse>>
    {
        public string Name { get; set; }
    }

    public class DeletePatientRepsonse 
    {
        public string Response { get; set; }
    }

    public class DeletePatientHandler : IRequestHandler<DeletePatientCommands, Result<DeletePatientRepsonse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePatientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DeletePatientRepsonse>> Handle(DeletePatientCommands request, CancellationToken cancellationToken)
        {
            var FindPatient = await _unitOfWork.PatientInfo.FindWithNameAsync(request.Name);

            if (FindPatient == null)
            {
                return Result.Failure<DeletePatientRepsonse>(PatientError.NotFound(request.Name));
            }

            // Corrected removal: Remove the single entity matching the name
            foreach (var patient in FindPatient.ToList())
            {
                _unitOfWork.PatientInfo.Remove(patient);
            }

            // Response
            var response = new DeletePatientRepsonse() { Response = "Patient deleted successfully" };

            // Save changes asynchronously
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
