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

        // default pagnigation params
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 5;
    }

    public class GetPatientByDateResponse
    {
        public List<Patient> Patients { get; set; }
        public PaginationsInfo Pagination { get; set; }
    }

    public class PaginationsInfo
    {
        public int TotalItems { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
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
            // convert to DateTime
            DateTime dateStart = DateTime.Parse(request.DateStart).ToUniversalTime();
            DateTime dateEnd = DateTime.Parse(request.DateEnd).ToUniversalTime();

            var totalItems = await _unitOfWork.Patient.GetTotalCountAsync().ConfigureAwait(false);

            // check valid date 

            if (dateStart > dateEnd)
            {
                return Result.Failure<GetPatientByDateResponse>(PatientError.InputDateInvalidFormat);
            }

            // how to get patient list depend on date Start and date End?
            var patients = await _unitOfWork.Patient.GetPatientByDateAsync(dateStart, dateEnd, request.Page, request.Limit).ConfigureAwait(false);

            return Result.Success(new GetPatientByDateResponse
            {
                Patients = patients.ToList(),

                Pagination = new PaginationsInfo
                {
                    TotalItems = totalItems,
                    TotalItemsPerPage = request.Page,
                    CurrentPage = request.Limit,
                    TotalPages = (int)Math.Ceiling((double)totalItems / request.Limit)
                }
            });
        }
    }
}
