using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship
{
    public class GetInternshipExcelFormQueryRequest : IRequest<GetInternshipExcelFormQueryResponse>
    {
        public Guid InternshipId { get; set; }
    }
}