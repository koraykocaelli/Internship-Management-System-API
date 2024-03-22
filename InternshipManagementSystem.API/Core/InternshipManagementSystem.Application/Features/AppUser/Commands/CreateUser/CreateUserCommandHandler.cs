using InternshipManagementSystem.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipManagementSystem.Application.Features.AppUser.Commands.CreateUser
{
    public class CreateUserCommandRequestHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandRequestResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandRequestHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        async Task<CreateUserCommandRequestResponse> IRequestHandler<CreateUserCommandRequest, CreateUserCommandRequestResponse>.Handle(
           CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Email = request.Email,

            }, request.Password);
            if (result.Succeeded)
            {
                return new() { Succeeded = true };
            }
            throw new UserCreateFailedException();

        }
    }
}
