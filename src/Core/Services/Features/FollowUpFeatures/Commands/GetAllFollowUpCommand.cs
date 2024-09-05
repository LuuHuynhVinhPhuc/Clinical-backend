using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.FollowUpsFeatures.Commands
{
    public class GetAllFollowUpCommand : IRequest<List<FollowUp>>
    {
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
            try
            {
                var FollowUps = await _unitOfWork.FollowUp.GetAllAsync();

                // Sort re-examinations by date in descending order to get the most recent ones first
                var sortedFollowUps = FollowUps.OrderByDescending(f => f.CreatedAt).ToList();

                return sortedFollowUps;
            }
            catch (System.NullReferenceException ex)
            {
                return new List<FollowUp>();
            }
        }
    }
}