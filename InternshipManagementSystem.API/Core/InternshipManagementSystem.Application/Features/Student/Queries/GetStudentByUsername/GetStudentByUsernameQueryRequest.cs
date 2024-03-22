using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Student
{
    public class GetStudentByUsernameQueryRequest : IRequest<GetStudentByUsernameQueryResponse>
    {
        public string Username { get; set; }
    }
}
