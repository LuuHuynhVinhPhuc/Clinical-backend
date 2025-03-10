using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.FollowUps;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Services.Features.FollowUpsFeatures.Commands
{
    public class CreateFollowUpCommand : IRequest<Result<FollowUpCreatedResponse>>
    {
        public Guid PatientId { get; set; }
        public string? Reason { get; set; }
        public string? History { get; set; }
        public string? Diagnosis { get; set; }
        public string? Summary { get; set; }
    }

    public class FollowUpCreatedResponse
    {
        public string Response { get; set; }
    }

    public class CreateFollowUpCommandHandler : IRequestHandler<CreateFollowUpCommand, Result<FollowUpCreatedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFollowUpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<FollowUpCreatedResponse>> Handle(CreateFollowUpCommand command, CancellationToken cancellationToken)
        {
            var existingPatient = await _unitOfWork.Patient.GetByIdAsync(command.PatientId).ConfigureAwait(false);
            if (existingPatient == null)
            {
                return Result.Failure<FollowUpCreatedResponse>(FollowUpErrors.NotFound(command.PatientId.ToString()));
            }

            var existingFollowUp = await _unitOfWork.FollowUp
                .GetByCondition(m => m.PatientId == command.PatientId &&
                                     m.Reason == command.Reason &&
                                     m.History == command.History &&
                                     m.Diagnosis == command.Diagnosis &&
                                     m.Summary == command.Summary)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            if (existingFollowUp != null)
            {
                return Result.Failure<FollowUpCreatedResponse>(FollowUpErrors.FollowUpExists);
            }

            var followUp = new FollowUp
            {
                PatientId = command.PatientId,
                Reason = command.Reason,
                History = command.History,
                Diagnosis = command.Diagnosis,
                Summary = command.Summary
            };

            // Add the FollowUp to the repository
            _unitOfWork.FollowUp.Add(followUp);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var response = new FollowUpCreatedResponse() { Response = "Follow-up created successfully" };

            return Result.Success(response);
        }
    }
}