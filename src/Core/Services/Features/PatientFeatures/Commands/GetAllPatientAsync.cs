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

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetAllPatientAsync : IRequest<IEnumerable<PatientsInfo>>
    {
    }

    // Task
    public class GetAllPatientHandler : IRequestHandler<GetAllPatientAsync, IEnumerable<PatientsInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPatientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PatientsInfo>> Handle(GetAllPatientAsync request, CancellationToken cancellationToken)
        {
            var PatientList = await _unitOfWork.PatientInfo.GetAllAsync();
            return PatientList.ToList();
        }
    }
}
