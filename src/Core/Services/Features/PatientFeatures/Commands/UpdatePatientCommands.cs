using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class UpdatePatientCommands : IRequest<Result<UpdatePatientResponse>>
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdatePatientResponse
    {
        public string Response { get; set; }
    }

    // Task
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommands, Result<UpdatePatientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdatePatientResponse>> Handle(UpdatePatientCommands request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.PatientInfo.GetByIdAsync(request.Id);
            if (patient == null) 
                return Result.Failure<UpdatePatientResponse>(PatientError.NotFound(request.PatientName));

            // reponse result
            var response = new UpdatePatientResponse() { Response = "Patient updated successfully" };

            // save changes 
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success(response);
        }
    }
}
