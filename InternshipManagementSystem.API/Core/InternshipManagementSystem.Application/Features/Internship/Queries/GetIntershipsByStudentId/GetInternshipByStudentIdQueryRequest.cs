using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship
{
    public class GetInternshipsByStudentIdQueryRequest : IRequest<GetInternshipsByStudentIdQueryResponse>
    {
        public Guid? StudentId { get; set; }
    }
}