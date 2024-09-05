using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.ReExaminations;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.ReExaminationFeatures.Commands
{
    public class EditFollowUpCommand : IRequest<Result<FollowUpEditedResponse>>
    {
        public Guid Id;
        public string? CheckUp { get; set; }
        public string? History { get; set; }
        public string? Diagnosis { get; set; }
    }

    public class FollowUpEditedResponse
    {
        public string Response { get; set; }
    }

    public class EditFollowUpCommandHandler : IRequestHandler<EditFollowUpCommand, Result<FollowUpEditedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditFollowUpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<FollowUpEditedResponse>> Handle(EditFollowUpCommand command, CancellationToken cancellationToken)
        {
            // Check if the ReExamination exists
            var existingFollowUp = await _unitOfWork.FollowUp.GetByIdAsync(command.Id);
            if (existingFollowUp == null)
            {
                return Result.Failure<FollowUpEditedResponse>(FollowUpErrors.NotFound(command.Id.ToString()));
            }

            // Update the existing ReExamination entity
            existingFollowUp.CheckUp = command.CheckUp;
            existingFollowUp.History = command.History;
            existingFollowUp.Diagnosis = command.Diagnosis;
            existingFollowUp.ModifiedAt = DateTime.UtcNow;

            var response = new FollowUpEditedResponse() { Response = "Follow-up edited successfully" };

            // Save changes to the repository
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}