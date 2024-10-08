using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.FollowUps;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.FollowUpsFeatures.Commands
{
    public class DeleteFollowUpCommand : IRequest<Result<FollowUpDeletedResponse>>
    {
        public Guid Id { get; set; } // Added Id to identify the ReExamination to delete
    }

    public class FollowUpDeletedResponse
    {
        public string Response { get; set; }
    }

    public class DeleteFollowUpCommandHandler : IRequestHandler<DeleteFollowUpCommand, Result<FollowUpDeletedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFollowUpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<FollowUpDeletedResponse>> Handle(DeleteFollowUpCommand command, CancellationToken cancellationToken)
        {
            var existingFollowUp = await _unitOfWork.FollowUp.GetByIdAsync(command.Id).ConfigureAwait(false);
            if (existingFollowUp == null)
            {
                return Result.Failure<FollowUpDeletedResponse>(FollowUpErrors.NotFound(command.Id.ToString()));
            }

            _unitOfWork.FollowUp.Remove(existingFollowUp);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var response = new FollowUpDeletedResponse() { Response = $"Successfully deleted follow-up ID {command.Id} " };
            return Result.Success(response);
        }
    }
}