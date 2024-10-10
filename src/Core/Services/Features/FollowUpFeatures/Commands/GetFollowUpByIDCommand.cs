using ClinicalBackend.Contracts.DTOs.FollowUp;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.FollowUps;
using Domain.Interfaces;
using MapsterMapper;
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
        public List<FollowUpDto> FollowUp { get; set; }
    }

    // task
    public class GetFollowUpByIdHandler : IRequestHandler<GetFollowUpByIdCommand, Result<GetFollowUpByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFollowUpByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetFollowUpByIdResponse>> Handle(GetFollowUpByIdCommand request, CancellationToken cancellationToken)
        {
            var followUps = await _unitOfWork.FollowUp.GetByCondition(m => m.PatientId == request.PatientId).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            if (_unitOfWork.FollowUp == null)
            {
                throw new InvalidOperationException("UnitOfWork or FollowUp repository is not initialized.");
            }

            if (followUps == null)
                return Result.Failure<GetFollowUpByIdResponse>(error: FollowUpErrors.FollowUpNotExists(request.PatientId));

            // return a list
            var res = new GetFollowUpByIdResponse
            {
                FollowUp = _mapper.Map<List<FollowUpDto>>(followUps)
            };

            return Result.Success(res);
        }
    }
}