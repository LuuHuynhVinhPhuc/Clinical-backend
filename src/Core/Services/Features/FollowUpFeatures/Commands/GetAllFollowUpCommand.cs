using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
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
        public List<FollowUp> FollowUps { get; set; }
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

        public GetAllFollowUpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<QueryFollowUpsResponse>> Handle(GetAllFollowUpCommand request, CancellationToken cancellationToken)
        {
            var followUps = await _unitOfWork.FollowUp.GetAllAsync().ConfigureAwait(false);
            var totalItems = await _unitOfWork.FollowUp.GetTotalCountAsync().ConfigureAwait(false);

            var response = new QueryFollowUpsResponse()
            {
                FollowUps = followUps.ToList(),
                Pagination = new PaginationInfo
                {
                    TotalItems = totalItems,
                    TotalItemsPerPage = request.PageSize,
                    CurrentPage = request.PageNumber,
                    TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
                }
            };

            return Result.Success(response);
        }
    }
}