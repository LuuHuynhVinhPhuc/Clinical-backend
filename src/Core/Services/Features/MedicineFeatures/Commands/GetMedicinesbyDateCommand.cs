using ClinicalBackend.Contracts.DTOs.Medicine;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using ClinicalBackend.Services.Features.PatientFeatures;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;
using System.Globalization;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicinesbyDateCommand : IRequest<Result<QueryMedicinesResponse<MedicineDto>>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        // default pagnigation params
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 5;
    }

    public class GetMedicinesbyDateHandler : IRequestHandler<GetMedicinesbyDateCommand, Result<QueryMedicinesResponse<MedicineDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicinesbyDateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<QueryMedicinesResponse<MedicineDto>>> Handle(GetMedicinesbyDateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime dateStart = DateTime.ParseExact(request.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.ToUniversalTime();
                DateTime dateEnd = DateTime.ParseExact(request.EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.AddDays(1).AddTicks(-1).ToUniversalTime();

                // check valid date

                if (dateStart > dateEnd)
                {
                    return Result.Failure<QueryMedicinesResponse<MedicineDto>>(PatientError.InputDateInvalidFormat);
                }

                var totalItems = await _unitOfWork.Medicines.GetTotalCountByDateAsync(dateStart, dateEnd).ConfigureAwait(false);

                var medicines = await _unitOfWork.Medicines.GetMedicinesByDateAsync(dateStart, dateEnd, request.Page, request.Limit).ConfigureAwait(false);

                var response = new QueryMedicinesResponse<MedicineDto>
                {
                    Medicines = _mapper.Map<List<MedicineDto>>(medicines),
                    Pagination = new PaginationInfo
                    {
                        TotalItems = totalItems,
                        TotalItemsPerPage = request.Limit,
                        CurrentPage = request.Page,
                        TotalPages = (int)Math.Ceiling((double)totalItems / request.Limit)
                    }
                };
                return Result.Success(response);
            }
            catch (FormatException)
            {
                return Result.Failure<QueryMedicinesResponse<MedicineDto>>(PatientError.InputDateInvalidFormat);
            }
        }
    }
}