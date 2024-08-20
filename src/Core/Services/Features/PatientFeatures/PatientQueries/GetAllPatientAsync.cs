using ClinicalBackend.Domain.Entities;
using MediatR;
using ClinicalBackend.Services.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace ClinicalBackend.Services.Features.PatientFeatures.PatientQueries
{
    public class GetAllPatientAsync : IRequest<Result<IEnumerable<PatientsInfo>>>
    {
    }

    // Task
    public class GetAllPatientHandler : IRequestHandler<GetAllPatientAsync, Result<IEnumerable<PatientsInfo>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public async Task<Result<IEnumerable<PatientsInfo>>> Handle(GetAllPatientAsync request, CancellationToken cancellationToken)
        {
            var PatientList = await _unitOfWork.PatientInfo.GetAllAsysnc();
            return Result.Success(PatientList);
        }
    }
}
