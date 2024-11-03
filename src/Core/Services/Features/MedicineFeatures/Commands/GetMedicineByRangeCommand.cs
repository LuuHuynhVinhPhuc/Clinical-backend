using ClinicalBackend.Contracts.DTOs.Medicine;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using ClinicalBackend.Services.Features.PatientFeatures;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;
using System.Globalization;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineByRangeCommand : IRequest<Result<QueryMedicinesResponse<MedicineByDateDto>>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetMedicineByRangeCommandHandler : IRequestHandler<GetMedicineByRangeCommand, Result<QueryMedicinesResponse<MedicineByDateDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicineByRangeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<QueryMedicinesResponse<MedicineByDateDto>>> Handle(GetMedicineByRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime dateStart = DateTime.ParseExact(request.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.ToUniversalTime();
                DateTime dateEnd = DateTime.ParseExact(request.EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.AddDays(1).AddTicks(-1).ToUniversalTime();

                if (dateStart > dateEnd)
                {
                    return Result.Failure<QueryMedicinesResponse<MedicineByDateDto>>(PatientError.InputDateInvalidFormat);
                }

                var prescriptions = await _unitOfWork.Prescription.GetByDateRangeAsync(dateStart, dateEnd).ConfigureAwait(false);

                var medicineSales = new Dictionary<Guid, Medicine>();

                foreach (var prescription in prescriptions)
                {
                    foreach (var product in prescription.Products)
                    {
                        if (!medicineSales.ContainsKey(product.MedicineId))
                        {
                            var originalMedicine = product.Medicine;
                            medicineSales[product.MedicineId] = new Medicine
                            {
                                Id = originalMedicine.Id,
                                Name = originalMedicine.Name,
                                Company = originalMedicine.Company,
                                Stock = 0,
                                Price = 0,
                                Type = originalMedicine.Type,
                                Status = originalMedicine.Status,
                                CreatedAt = originalMedicine.CreatedAt,
                                ModifiedAt = originalMedicine.ModifiedAt
                            };
                        }

                        var current = medicineSales[product.MedicineId];
                        current.Stock += product.Quantity;
                        current.Price = current.Stock * product.Medicine.Price;
                        medicineSales[product.MedicineId] = current;
                    }
                }

                var totalCount = medicineSales.Count;
                var topMedicines = medicineSales.Values
                    .OrderByDescending(m => m.Stock)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var response = new QueryMedicinesResponse<MedicineByDateDto>
                {
                    Medicines = _mapper.Map<List<MedicineByDateDto>>(topMedicines),
                    Pagination = new PaginationInfo
                    {
                        TotalItems = totalCount,
                        TotalItemsPerPage = request.PageSize,
                        CurrentPage = request.PageNumber,
                        TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
                    }
                };

                return Result.Success(response);
            }
            catch (FormatException)
            {
                return Result.Failure<QueryMedicinesResponse<MedicineByDateDto>>(PatientError.InputDateInvalidFormat);
            }
        }
    }
}