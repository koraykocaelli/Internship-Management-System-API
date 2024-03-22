using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Queries.GetInternshipByInternshipId
{
    public class GetInternshipByInternshipIdQueryRequest : IRequest<GetInternshipByInternshipIdQueryResponse>
    {
        public Guid InternshipId { get; set; }
    }
}