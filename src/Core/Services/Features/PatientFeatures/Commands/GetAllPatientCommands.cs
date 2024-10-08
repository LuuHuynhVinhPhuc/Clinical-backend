﻿using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using System.Globalization;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetAllPatientCommands : IRequest<Result<GetAllPatientResponse>>
    {
        // default pagnigation params
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 5;
    }

    public class GetAllPatientResponse()
    {
        public List<Patient> Patients { get; set; }
        public PaginationInfo Pagination { get; set; }
    }
    public class PaginationInfo
    {
        public int TotalItems { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
    // Task
    public class GetAllPatientHandler : IRequestHandler<GetAllPatientCommands, Result<GetAllPatientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPatientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<GetAllPatientResponse>> Handle(GetAllPatientCommands request, CancellationToken cancellationToken)
        {
            // get all patient
            var patients = await _unitOfWork.Patient.GetAllAsync(request.Page, request.Limit).ConfigureAwait(false);
            var totalItems = await _unitOfWork.Patient.GetTotalCountAsync().ConfigureAwait(false);

            var res = new GetAllPatientResponse()
            {
                Patients = patients.Select(p => new Patient
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    DOB = ConvertToDateOnly(p.DOB),
                    Address = p.Address,
                    PhoneNumber = p.PhoneNumber,
                    CreatedAt = p.CreatedAt,
                    ModifiedAt = p.ModifiedAt,
                    CheckStatus = p.CheckStatus,
                    FollowUps = p.FollowUps
                }).ToList(),

                Pagination = new PaginationInfo
                {
                    TotalItems = totalItems,
                    TotalItemsPerPage = request.Page,
                    CurrentPage = request.Limit,
                    TotalPages = (int)Math.Ceiling((double)totalItems / request.Limit)
                }
            };

            return Result.Success(res);
        }

        private DateOnly ConvertToDateOnly(DateOnly dateTime)
        {
            // Return parsed date if successful, otherwise return original dateTime
            DateOnly res = DateOnly.ParseExact(dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return res;
        }
    }
}