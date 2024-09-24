using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.FollowUps;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Services.Features.FollowUpFeatures.Commands
{
    public class GetFollowUpByIdCommand : IRequest<Result<GetFollowUpByIdResponse>>
    {
        public Guid PatientId { get; set; }
    }

    public class GetFollowUpByIdResponse
    {
        public List<FollowUp> FollowUp { get; set; }
    }

    // task
    public class GetFollowUpByIdHandler : IRequestHandler<GetFollowUpByIdCommand, Result<GetFollowUpByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFollowUpByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetFollowUpByIdResponse>> Handle(GetFollowUpByIdCommand request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.FollowUp.GetByCondition(m => m.PatientId == request.PatientId).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false); ;

            if (_unitOfWork.FollowUp == null)
            {
                throw new InvalidOperationException("UnitOfWork or FollowUp repository is not initialized.");
            }

            if (patient == null)
                return Result.Failure<GetFollowUpByIdResponse>(error: FollowUpErrors.FollowUpNotExists(request.PatientId));

            // return a list
            var res = new GetFollowUpByIdResponse
            {
                FollowUp = new List<FollowUp> { patient }
            };

            return Result.Success(res);
        }
    }
}