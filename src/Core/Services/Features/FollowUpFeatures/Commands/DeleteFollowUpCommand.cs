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
            // Check if the FollowUp exists
            var existingFollowUp = await _unitOfWork.FollowUp.GetByIdAsync(command.Id).ConfigureAwait(false);
            if (existingFollowUp == null)
            {
                return Result.Failure<FollowUpDeletedResponse>(FollowUpErrors.NotFound(command.Id.ToString()));
            }

            // Remove the existing FollowUp entity
            _unitOfWork.FollowUp.Remove(existingFollowUp);
            // Save changes to the repository
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var response = new FollowUpDeletedResponse() { Response = "Re-Examination deleted successfully" };
            return Result.Success(response);
        }
    }
}