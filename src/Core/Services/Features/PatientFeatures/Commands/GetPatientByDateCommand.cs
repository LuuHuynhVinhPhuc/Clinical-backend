using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using ClinicalBackend.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByDateCommand : IRequest<Result<GetPatientByDateResponse>>
    {
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
    }

    public class GetPatientByDateResponse
    {
        public int TotalPatient { get; set; }
        public List<Patient> Patients { get; set; }
    }

    public class GetPatientByDateHandler : IRequestHandler<GetPatientByDateCommand, Result<GetPatientByDateResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPatientByDateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPatientByDateResponse>> Handle(GetPatientByDateCommand request, CancellationToken cancellationToken)
        {

            var dateStart = DateTime.ParseExact(request.DateStart, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            var dateEnd = DateTime.ParseExact(request.DateEnd, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);


            var patients = await _unitOfWork.Patient.GetByCondition(p =>
                p.CreatedAt.Date >= dateStart
                && p.CreatedAt.Date <= dateEnd
                && p.CheckStatus == "examined").ToListAsync(cancellationToken).ConfigureAwait(false);

            return Result.Success<GetPatientByDateResponse>(new GetPatientByDateResponse
            {
                TotalPatient = patients.Count,
                Patients = patients.ToList()
            });
        }
    }
}
