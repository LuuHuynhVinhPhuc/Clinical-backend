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
    public class UpdatePatientCommands : IRequest<Result<UpdatePatientResponse>>
    {
        public int id { get; set; }
        public required string PatientName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }

    // Response 
    public class UpdatePatientResponse
    {
        public string Response { get; set; }
    }

    // Task
    public class UpdatePatientHandler : IRequestHandler<UpdatePatientCommands, Result<UpdatePatientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdatePatientResponse>> Handle(UpdatePatientCommands request, CancellationToken cancellationToken)
        {
            // check exist 
            var existPatient = await _unitOfWork.PatientInfo.GetByIdAsync(request.id);
            if (existPatient == null)
                return Result.Failure<UpdatePatientResponse>(PatientError.NotFound(request.PatientName));

            // Update Patient info and Save it 
            existPatient.PatientName = request.PatientName;
            existPatient.Age = request.Age;
            existPatient.Address = request.Address;
            existPatient.PhoneNumber = request.PhoneNumber;

            // Response for success task
            var responesTxt = new UpdatePatientResponse() { Response = "Patient updated successfully" };

            // Save changes to the responsitory
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(responesTxt);

            //throw new notimplementedexception();
        }
    }
}
