using ClinicalBackend.Domain.Common;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Constants;
using ClinicalBackend.Services.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Services.Features.UserFeatures.Commands
{
    public class CreateUserCommand : IRequest<Result<UserCreatedResponse>>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class UserCreatedResponse
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateUserCommand, Result<UserCreatedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<UserCreatedResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            User? existingUser = await _unitOfWork.Users.GetByCondition(u => u.UserName == command.Name).FirstAsync();

            if (!(existingUser is null))
            {
                return Result.Failure<UserCreatedResponse>(UserErrors.UserNameExist);
            }

            var user = new User()
            {
                UserName = command.Name,
                HashPassword = BCrypt.Net.BCrypt.HashPassword(command.Password),
                RoleId = (int)ROLEL.User
            };

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();

            string token = _jwtProvider.Generate(user);

            return Result.Success<UserCreatedResponse>(new UserCreatedResponse() { Id = user.Id, Token = token });
        }
    }
}
