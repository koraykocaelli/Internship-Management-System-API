using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Student.Commands.AddStudentToAdvisor
{
    public class AddStudentToAdvisorRequest : IRequest<AddStudentToAdvisorResponse>
    {
        public Guid AdvisorID { get; set; }

        public Guid StudentID { get; set; }
    }
}
