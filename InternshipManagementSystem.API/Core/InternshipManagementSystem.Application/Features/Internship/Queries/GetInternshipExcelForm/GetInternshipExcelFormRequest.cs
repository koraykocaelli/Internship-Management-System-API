using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Queries.GetInternshipExcelForm
{
    public class GetInternshipExcelFormRequest : IRequest<GetInternshipExcelFormResponse>
    {
        public Guid InternshipId { get; set; }
    }
}