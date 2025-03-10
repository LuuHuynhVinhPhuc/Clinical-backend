using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.FollowUps;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.FollowUpsFeatures.Commands
{
    public class EditFollowUpCommand : IRequest<Result<FollowUpEditedResponse>>
    {
        public Guid Id { get; set; }
        public string? Reason { get; set; }
        public string? History { get; set; }
        public string? Diagnosis { get; set; }
        public string? Summary { get; set; } // Added Summary property
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
            var existingFollowUp = await _unitOfWork.FollowUp.GetByIdAsync(command.Id).ConfigureAwait(false);
            if (existingFollowUp == null)
            {
                return Result.Failure<FollowUpEditedResponse>(FollowUpErrors.NotFound(command.Id.ToString()));
            }

            existingFollowUp.Reason = command.Reason;
            existingFollowUp.History = command.History;
            existingFollowUp.Diagnosis = command.Diagnosis;
            existingFollowUp.Summary = command.Summary;

            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var response = new FollowUpEditedResponse() { Response = "Follow-up edited successfully" };

            return Result.Success(response);
        }
    }
}