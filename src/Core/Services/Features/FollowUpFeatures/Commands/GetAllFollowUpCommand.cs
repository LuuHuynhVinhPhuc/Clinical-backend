using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.FollowUpsFeatures.Commands
{
    public class GetAllFollowUpCommand : IRequest<(List<FollowUp>, int)>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllFollowUpCommandHandler : IRequestHandler<GetAllFollowUpCommand, (List<FollowUp>, int)>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllFollowUpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(List<FollowUp>, int)> Handle(GetAllFollowUpCommand request, CancellationToken cancellationToken)
        {
            var followUps = await _unitOfWork.FollowUp.GetAllAsync().ConfigureAwait(false);
            var totalFollowUps = followUps.Count();

            var paginatedFollowUps = followUps
                .OrderByDescending(f => f.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return (paginatedFollowUps, totalFollowUps);
        }
    }
}