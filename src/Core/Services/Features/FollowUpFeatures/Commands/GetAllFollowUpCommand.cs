using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.FollowUpsFeatures.Commands
{
    public class GetAllFollowUpCommand : IRequest<List<FollowUp>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllFollowUpCommandHandler : IRequestHandler<GetAllFollowUpCommand, List<FollowUp>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllFollowUpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<FollowUp>> Handle(GetAllFollowUpCommand request, CancellationToken cancellationToken)
        {
            var followUps = await _unitOfWork.FollowUp.GetAllAsync();

            var paginatedFollowUps = followUps
                .OrderByDescending(f => f.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return paginatedFollowUps;
        }
    }
}