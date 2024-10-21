using ClinicalBackend.Contracts.DTOs.FollowUp;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.FollowUps;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.FollowUpFeatures.Commands
{
    public class GetFollowUpByIdCommand : IRequest<Result<GetFollowUpByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetFollowUpByIdResponse
    {
        public FollowUpDto FollowUp { get; set; }
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
            var followUp = await _unitOfWork.FollowUp
                                .GetByIdAsync(request.Id)
                                .ConfigureAwait(false);

            if (_unitOfWork.FollowUp == null)
            {
                throw new InvalidOperationException("UnitOfWork or FollowUp repository is not initialized.");
            }

            if (followUp == null)
                return Result.Failure<GetFollowUpByIdResponse>(error: FollowUpErrors.FollowUpNotExists(request.Id));

            // return a list
            var res = new GetFollowUpByIdResponse
            {
                FollowUp = _mapper.Map<FollowUpDto>(followUp)
            };

            return Result.Success(res);
        }
    }
}