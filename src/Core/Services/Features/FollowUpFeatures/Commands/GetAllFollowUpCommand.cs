using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.ReExaminationFeatures.Commands
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
            var FollowUps = await _unitOfWork.FollowUp.GetAllAsync();
            if (FollowUps == null || !FollowUps.Any())
            {
                return new List<FollowUp>(); // Return an empty list if no re-examinations found
            }

            // Sort re-examinations by date in descending order to get the most recent ones first
            var sortedFollowUps = FollowUps.OrderByDescending(f => f.dateCreated).ToList();

            return sortedFollowUps;
        }
    }
}