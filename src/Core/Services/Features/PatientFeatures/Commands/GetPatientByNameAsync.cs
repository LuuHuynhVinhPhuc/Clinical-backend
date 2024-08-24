using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByNameAsync : IRequest<List<PatientsInfo>>
    {
        public string Name { get; set; }
    }

    // Task
    public class GetPatientBIDHandler : IRequestHandler<GetPatientByNameAsync, List<PatientsInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPatientBIDHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PatientsInfo>> Handle(GetPatientByNameAsync request, CancellationToken cancellationToken)
        {
            // Get Name and compare it 
            var FindPatient = await _unitOfWork.PatientInfo.FindWithNameAsync(request.Name);
            // return value ( check null first )
            return FindPatient ?? new List<PatientsInfo> { };
        }
    }
}
