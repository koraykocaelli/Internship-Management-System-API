using InternshipManagementSystem.Domain.Entities;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Commands
{
    public class UpdateInternshipCommandRequest : IRequest<UpdateInternshipCommandResponse>
    {

        public Guid InternshipID { get; set; }
        public string? StudentNo { get; set; }
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public Guid? InternshipApplicationFormID { get; set; }
        public Guid? InternshipApplicationExelFormID { get; set; }
        public Guid? InternshipBookID { get; set; }
        public Guid? SPASID { get; set; }
    }
}