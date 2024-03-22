using MediatR;

namespace InternshipManagementSystem.Application.Features.Student
{
    public class DeleteStudentByIdCommandRequest : IRequest<DeleteStudentByIdCommandResponse>
    {
       public Guid Id { get; set; }
    }
}