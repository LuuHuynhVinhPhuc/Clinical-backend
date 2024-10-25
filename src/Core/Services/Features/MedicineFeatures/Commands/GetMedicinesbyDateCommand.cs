
using Domain.Interfaces;
using ClinicalBackend.Contracts.DTOs.Medicine;
using MediatR;
using MapsterMapper;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.PatientFeatures.Commands;
using ClinicalBackend.Services.Features.PatientFeatures;
using System.Globalization;
using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Domain.Entities;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicinesbyDateCommand : IRequest<Result<MedicinesbyDateResponse>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        // default pagnigation params
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 5;
    }

    public class MedicinesbyDateResponse
    {
        public List<MedicineDto> Medicines { get; set; }
        public PaginationsInfo Pagination { get; set; }
    }

    public class PaginationsInfo
    {
        public int TotalItems { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class GetMedicinesbyDateHandler : IRequestHandler<GetMedicinesbyDateCommand, Result<MedicinesbyDateResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetMedicinesbyDateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<MedicinesbyDateResponse>> Handle(GetMedicinesbyDateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime dateStart = DateTime.ParseExact(request.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.ToUniversalTime();
                DateTime dateEnd = DateTime.ParseExact(request.EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.AddDays(1).AddTicks(-1).ToUniversalTime();

                // check valid date

                if (dateStart > dateEnd)
                {
                    return Result.Failure<MedicinesbyDateResponse>(PatientError.InputDateInvalidFormat);
                }

                var totalItems = await _unitOfWork.Medicines.GetTotalCountByDateAsync(dateStart, dateEnd).ConfigureAwait(false);

                var medicines = await _unitOfWork.Medicines.GetMedicinesByDateAsync(dateStart, dateEnd, request.Page, request.Limit).ConfigureAwait(false);

                return Result.Success(new MedicinesbyDateResponse
                {
                    Medicines = _mapper.Map<List<MedicineDto>>(medicines),
                    Pagination = new PaginationsInfo
                    {
                        TotalItems = totalItems,
                        TotalItemsPerPage = request.Page,
                        CurrentPage = request.Limit,
                        TotalPages = (int)Math.Ceiling((double)totalItems / request.Limit)
                    }
                });

            }
            catch(FormatException) 
            {
                return Result.Failure<MedicinesbyDateResponse>(PatientError.InputDateInvalidFormat);
            }
        }
    }
}
