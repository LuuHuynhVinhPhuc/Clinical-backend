using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Services.Features.ReExaminations.Commands
{
    public class CreateFollowUpCommand : IRequest<Result<FollowUpCreatedResponse>>
    {
        public PatientsInfo Patient { get; set; }

        //Tổng quát
        public string? CheckUp { get; set; }

        //Tiền căn
        public string? History { get; set; }

        //Chuẩn đoán
        public string? Diagnosis { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateModified { get; set; }

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
            // Check if the medicine already exists
            var existingFollowUp = await _unitOfWork.FollowUp.GetByCondition(m => m.Patient == command.Patient).FirstOrDefaultAsync(cancellationToken);
            if (existingFollowUp != null)
            {
                return Result.Failure<FollowUpCreatedResponse>(FollowUpErrors.FollowUpExists);
            }

            // Create a new Medicine entity
            var FollowUp = new FollowUp
            {
                Patient = command.Patient,
                CheckUp = command.CheckUp,
                History = command.History,
                Diagnosis = command.Diagnosis,
                dateCreated = DateTime.UtcNow,
                dateModified = DateTime.UtcNow
            };

            var response = new FollowUpCreatedResponse() { Response = "Follow Up created successfully" };

            // Add the medicine to the repository
            _unitOfWork.FollowUp.Add(FollowUp);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}