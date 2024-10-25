using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using ClinicalBackend.Contracts.DTOs.Medicine;
using MediatR;
using MapsterMapper;
using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicinesbyDateCommand : IRequest<List<MedicinesbyDateResponse>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

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

    // Handler 
    //public class GetMedicinesbyDateHandler : IRequestHandler<GetMedicinesbyDateCommand, Result<MedicinesbyDateResponse>>
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMapper _mapper;

    //    public GetMedicinesbyDateHandler(IUnitOfWork unitOfWork, IMapper mapper)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _mapper = mapper;
    //    }

    //    public Task<Result<MedicinesbyDateResponse>> Handle(GetMedicinesbyDateCommand request, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
