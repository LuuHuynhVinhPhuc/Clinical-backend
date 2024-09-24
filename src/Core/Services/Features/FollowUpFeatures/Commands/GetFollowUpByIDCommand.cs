using ClinicalBackend.Services.Common;
using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicalBackend.Services.Features.FollowUps;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClinicalBackend.Services.Features.FollowUpFeatures.Commands
{
    public class GetFollowUpByIDCommand : IRequest<Result<GetFollowUPbyIDResponse>>
    {
        public Guid PatientId { get; set; }
       
    }

    public class GetFollowUPbyIDResponse
    {
        public List<FollowUp> FollowUpDetails { get; set; }
    }

    // task 
    public class GetFollowUpByIDHandler : IRequestHandler<GetFollowUpByIDCommand, Result<GetFollowUPbyIDResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFollowUpByIDHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetFollowUPbyIDResponse>> Handle(GetFollowUpByIDCommand request, CancellationToken cancellationToken)
        {
            // find with id 
            var patient = await _unitOfWork.FollowUp.GetByCondition(m => m.PatientId == request.PatientId).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false); ;
            
            // Check if _unitOfWork or FollowUp is null
            if (_unitOfWork?.FollowUp == null)
            {
                throw new InvalidOperationException("UnitOfWork or FollowUp repository is not initialized.");
            }
            // check if not exist => this patient do not have a follow check
            if (patient == null)
                return Result.Failure<GetFollowUPbyIDResponse>(error: FollowUpErrors.FollowUpNotExists(request.PatientId));
            // return a list 
            var res = new GetFollowUPbyIDResponse
            {
                FollowUpDetails = new List<FollowUp> { patient }
            };

            return Result.Success(res);

        }
    }
}
