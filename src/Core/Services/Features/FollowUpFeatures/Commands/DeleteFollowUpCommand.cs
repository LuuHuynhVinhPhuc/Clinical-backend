using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.ReExaminations;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.ReExaminationFeatures.Commands
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
            // Check if the ReExamination exists
            var existingFollowUp = await _unitOfWork.FollowUp.GetByIdAsync(command.Id);
            if (existingFollowUp == null)
            {
                return Result.Failure<FollowUpDeletedResponse>(FollowUpErrors.NotFound(command.Id.ToString()));
            }

            // Remove the existing ReExamination entity
            _unitOfWork.FollowUp.Remove(existingFollowUp);
            // Save changes to the repository
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = new FollowUpDeletedResponse() { Response = "Re-Examination deleted successfully" };
            return Result.Success(response);
        }
    }
}