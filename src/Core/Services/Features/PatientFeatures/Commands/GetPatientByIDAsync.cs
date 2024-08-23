using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByIDAsync : IRequest<IEnumerable<PatientsInfo>>
    {
    }

    // Task
    public class GetPatientBIDHandler : IRequestHandler<GetAllPatientAsync, IEnumerable<PatientsInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPatientBIDHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<PatientsInfo>> Handle(GetAllPatientAsync request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
