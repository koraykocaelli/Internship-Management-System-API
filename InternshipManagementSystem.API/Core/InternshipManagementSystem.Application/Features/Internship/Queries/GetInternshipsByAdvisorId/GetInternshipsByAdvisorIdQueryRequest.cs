using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship
{
    public class GetInternshipsByAdvisorIdQueryRequest : IRequest<GetInternshipsByAdvisorIdQueryResponse>
    {
        public Guid? AdvisorId { get; set; }

    }
}
