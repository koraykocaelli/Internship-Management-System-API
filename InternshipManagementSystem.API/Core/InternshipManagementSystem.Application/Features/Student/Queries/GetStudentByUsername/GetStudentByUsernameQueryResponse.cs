using InternshipManagementSystem.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Student
{

    public class GetStudentByUsernameQueryResponse
    {
        public string Message { get; set; }
        public ResponseModel Response { get; set; }
    }
}
