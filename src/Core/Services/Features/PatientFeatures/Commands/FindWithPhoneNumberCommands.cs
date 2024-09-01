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
    public class FindWithPhoneNumberCommands  : IRequest<List<Patient>>
    {
        public string Phonenumber { get; set; }
    }

    // Task
    public class FindWithPhoneNumberHandler : IRequestHandler<FindWithPhoneNumberCommands, List<Patient>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FindWithPhoneNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Patient>> Handle(FindWithPhoneNumberCommands request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patient.FindWithPhoneNumberAsync(request.Phonenumber);
            // return value to list
            return patient ?? new List<Patient>();
        }
    }
}
