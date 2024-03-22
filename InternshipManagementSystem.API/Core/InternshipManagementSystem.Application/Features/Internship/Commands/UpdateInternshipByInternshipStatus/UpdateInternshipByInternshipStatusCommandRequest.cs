using InternshipManagementSystem.Domain.Entities;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Commands
{
    public class UpdateInternshipByInternshipStatusCommandRequest : IRequest<UpdateInternshipByInternshipStatusCommandResponse>
    {
        public Guid InternshipID { get; set; }
        public InternshipStatus Status { get; set; }
    }
}
