using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Services.Features.UserFeatures.Commands
{
    public class LoginUserCommand : IRequest<Result<UserLoginResponse>>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UserLoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<UserLoginResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            User? existingUser;
            try
            {
                existingUser = await _unitOfWork
                        .Users
                        .GetByCondition(u => u.UserName == command.Name)
                        .Include(u => u.Role)
                        .FirstAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Failure<UserLoginResponse>(new Error("unknown", ex.Message));
            }

            if (existingUser is null || !BCrypt.Net.BCrypt.Verify(command.Password, existingUser.HashPassword))
            {
                return Result.Failure<UserLoginResponse>(UserErrors.IncorrectLoginInfo);
            }

            string token = _jwtProvider.Generate(existingUser);

            return Result.Success(new UserLoginResponse() { Id = existingUser.Id, Token = token });
        }
    }
}
