using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.AppUser.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {
    }
    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        //internal DTO.Token token;

        public DTO.Token Token { get; internal set; }

        public Guid UserID { get; set; }
        public string? UserTypeName { get; set; }

    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }

    }

}
