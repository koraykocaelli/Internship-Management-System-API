using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Student
{
    public class GetStudentByAdvisorIdQueryRequest : IRequest<GetStudentByAdvisorIdQueryResponse>
    {
        public Guid AdvisorId { get; set; }
    }
}
