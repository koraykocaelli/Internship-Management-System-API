using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.Token;
using InternshipManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace InternshipManagementSystem.Application.Features.AppUser.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {


        readonly ITokenHandler _tokenHandler;
        readonly UserManager<Domain.Entities.Identity.AppUser> _usermanager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signinmanager;
        readonly IStudentReadRepository _studentReadRepository;
        private readonly IStudentWriteRepository _studentWriteRepository;
        readonly IAdvisorReadRepository _advisorReadRepository;
        private readonly IAdvisorWriteRepository _advisorWriteRepository;

        public LoginUserCommandHandler(
            UserManager<Domain.Entities.Identity.AppUser> usermanager,
            SignInManager<Domain.Entities.Identity.AppUser> signinmanager,
            IStudentReadRepository studentReadRepository,
            IStudentWriteRepository studentWriteRepository,
            IAdvisorReadRepository advisorReadRepository,
            IAdvisorWriteRepository advisorWriteRepository,
            ITokenHandler tokenHandler
            )
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _studentReadRepository = studentReadRepository;
            _studentWriteRepository = studentWriteRepository;
            _advisorReadRepository = advisorReadRepository;
            _advisorWriteRepository = advisorWriteRepository;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _usermanager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                if (await HandleAppUserCreateAndSignIn(request, cancellationToken))
                {
                    try
                    {
                        var token = _tokenHandler.CreateAccesstoken(5);
                        return new LoginUserSuccessCommandResponse()
                        {
                            Token = token,
                            UserID = Guid.Parse(user.Id),
                            UserTypeName = (await _usermanager.GetRolesAsync(user)).First()
                        };

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            else
            {
                var result = await _usermanager.CheckPasswordAsync(user, request.Password);
                if (result)
                {
                    try
                    {
                        await _signinmanager.SignInAsync(user,true);
                        var token = _tokenHandler.CreateAccesstoken(5);
                        return new LoginUserSuccessCommandResponse()
                        {
                            Token = token,
                            UserID = Guid.Parse(user.Id),
                            UserTypeName = (await _usermanager.GetRolesAsync(user)).First()
                        };
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


                };
            }

            return new LoginUserErrorCommandResponse() { Message = "Login Unsuccessful" };
        }

        private async Task<bool> HandleAppUserCreateAndSignIn(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            if (IsEmail(request.UserName))
            {
                var advisor = await _advisorReadRepository.Table.FirstOrDefaultAsync(a => a.Email == request.UserName, cancellationToken: cancellationToken);

                if (advisor != null && request.Password == advisor.TC_NO)
                {
                    await CreateAppUser(advisor, request, "Advisor");
                    return true;
                }
                else
                {
                    throw new ApplicationException("Advisor not found or password is incorrect");
                }
            }
            else if (request.UserName.Length == 11 && long.TryParse(request.UserName, out var x))
            {                                 
                var student = await _studentReadRepository.Table.FirstOrDefaultAsync(s => s.StudentNo == request.UserName);

                if (student != null && request.Password == student.TC_NO)
                {
                    await CreateAppUser(student, request, "Student");
                    return true;
                }
                else
                {
                    throw new ApplicationException("Student not found or password is incorrect");
                }
            }
            else
            {
                throw new ApplicationException("Username invalid");
            }
        }

        private async Task CreateAppUser(IAppUserCreatable user, LoginUserCommandRequest loginRequest, string roleType)
        {
            var result = await _usermanager.CreateAsync(new Domain.Entities.Identity.AppUser()
            {
                Id = user.GetGuidID().ToString(),
                UserName = user.GetUniqueIdentifier(),
            }, loginRequest.Password);

            var appUser = await _usermanager.FindByNameAsync(loginRequest.UserName);
            try
            {
                await _usermanager.AddToRoleAsync(appUser, roleType);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            await _signinmanager.SignInAsync(appUser!, true);
        }

        private bool IsEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            Regex regex = new Regex(emailPattern);

            return regex.IsMatch(email);
        }
    }
}
