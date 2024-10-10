using ClinicalBackend.Contracts.DTOs.FollowUp;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.FollowUpsFeatures.Commands
{
    public class GetAllFollowUpCommand : IRequest<Result<QueryFollowUpsResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class QueryFollowUpsResponse
    {
        public List<FollowUpDto> FollowUps { get; set; }
        public PaginationInfo Pagination { get; set; }
    }

    public class PaginationInfo
    {
        public int TotalItems { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class GetAllFollowUpCommandHandler : IRequestHandler<GetAllFollowUpCommand, Result<QueryFollowUpsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllFollowUpCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<QueryFollowUpsResponse>> Handle(GetAllFollowUpCommand request, CancellationToken cancellationToken)
        {
            var followUps = await _unitOfWork.FollowUp.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken).ConfigureAwait(false);
            var totalItems = await _unitOfWork.FollowUp.GetTotalCountAsync().ConfigureAwait(false);

            var response = new QueryFollowUpsResponse()
            {
                FollowUps = _mapper.Map<List<FollowUpDto>>(followUps),
                Pagination = new PaginationInfo
                {
                    TotalItems = totalItems,
                    TotalItemsPerPage = request.PageSize,
                    CurrentPage = request.PageNumber,
                    TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
                },
            };

            return Result.Success(response);
        }
    }
}