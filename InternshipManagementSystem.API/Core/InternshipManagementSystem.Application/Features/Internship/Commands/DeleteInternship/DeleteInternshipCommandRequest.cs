using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Commands.DeleteInternship
{
    public class DeleteInternshipCommandRequest : IRequest<DeleteInternshipCommandResponse> 
    {
        public Guid Id { get; set; }
    }
}