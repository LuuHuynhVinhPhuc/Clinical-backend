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
    public class GetMedicineByTop10Command : IRequest<Result<Top10MedicineResponse>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class Top10MedicineResponse {
        public List<Top10MedicineDto> Medicines { get; set; }
    }

    public class GetMedicineByTop10CommandHandler : IRequestHandler<GetMedicineByTop10Command, Result<Top10MedicineResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicineByTop10CommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Top10MedicineResponse>> Handle(GetMedicineByTop10Command request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime dateStart = DateTime.ParseExact(request.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.ToUniversalTime();
                DateTime dateEnd = DateTime.ParseExact(request.EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.AddDays(1).AddTicks(-1).ToUniversalTime();

                if (dateStart > dateEnd)
                {
                    return Result.Failure<Top10MedicineResponse>(PatientError.InputDateInvalidFormat);
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

                var top10Medicines = medicineSales.Values
                    .OrderByDescending(m => m.Stock)
                    .Take(10)
                    .ToList();

                var response = new Top10MedicineResponse
                {
                    Medicines = _mapper.Map<List<Top10MedicineDto>>(top10Medicines),
                };

                return Result.Success(response);
            }
            catch (FormatException)
            {
                return Result.Failure<Top10MedicineResponse>(PatientError.InputDateInvalidFormat);
            }
        }
    }
}
