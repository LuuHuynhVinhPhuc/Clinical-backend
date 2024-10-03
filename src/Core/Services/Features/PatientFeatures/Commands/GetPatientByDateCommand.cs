using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using ClinicalBackend.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            DateOnly dateStart, dateEnd;

            if (!DateOnly.TryParseExact(request.DateStart, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateStart) ||
                !DateOnly.TryParseExact(request.DateEnd, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateEnd))
            {
                return Result.Failure<GetPatientByDateResponse>(PatientError.InputDateInvalidFormat);
            }

            if (dateStart > dateEnd)
            {
                return Result.Failure<GetPatientByDateResponse>(PatientError.InvalidDateRange);
            }

            var patients = await _unitOfWork.Patient.GetByCondition(p =>
                p.CreatedAt.Date >= dateStart.ToDateTime(TimeOnly.MinValue)
                && p.CreatedAt.Date <= dateEnd.ToDateTime(TimeOnly.MinValue)
                && p.CheckStatus == "examined").ToListAsync(cancellationToken).ConfigureAwait(false);

            return Result.Success(new GetPatientByDateResponse
            {
                TotalPatient = patients.Count,
                Patients = patients
            });
        }
    }
}
