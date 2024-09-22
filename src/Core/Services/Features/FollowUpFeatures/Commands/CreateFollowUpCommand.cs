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

        //Tổng quát
        public string? CheckUp { get; set; }

        //Tiền căn
        public string? History { get; set; }

        //Chuẩn đoán
        public string? Diagnosis { get; set; }
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
            // Check if the FollowUp already exists
            var existingPatient = await _unitOfWork.Patient.GetByIdAsync(command.PatientId).ConfigureAwait(false);
            if (existingPatient == null)
            {
                return Result.Failure<FollowUpCreatedResponse>(FollowUpErrors.NotFound(command.PatientId.ToString()));
            }

            var existingFollowUp = await _unitOfWork.FollowUp.GetByCondition(m => m.PatientId == command.PatientId).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (existingFollowUp != null)
            {
                return Result.Failure<FollowUpCreatedResponse>(FollowUpErrors.FollowUpExists);
            }

            // Create a new FollowUp entity
            var FollowUp = new FollowUp
            {
                PatientId = command.PatientId,
                CheckUp = command.CheckUp,
                History = command.History,
                Diagnosis = command.Diagnosis,
            };


            // Add the medicine to the repository
            _unitOfWork.FollowUp.Add(FollowUp);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var response = new FollowUpCreatedResponse() { Response = "Follow-up created successfully" };

            return Result.Success(response);
        }
    }
}