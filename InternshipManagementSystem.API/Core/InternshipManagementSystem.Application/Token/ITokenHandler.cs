using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Token
{
    public interface ITokenHandler
    {
       DTO.Token CreateAccesstoken(int minute);
    }
}
