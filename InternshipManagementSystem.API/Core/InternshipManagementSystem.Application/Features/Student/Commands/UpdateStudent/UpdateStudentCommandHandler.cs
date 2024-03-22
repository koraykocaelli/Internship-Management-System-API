using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Student
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommandRequest, UpdateStudentCommandResponse>
    {
        private readonly IStudentWriteRepository _studentWriteRepository;
        private readonly IStudentReadRepository _studentReadRepository;


        public  Task<UpdateStudentCommandResponse> Handle(UpdateStudentCommandRequest request, CancellationToken cancellationToken)
        {


            //student.TC_NO = model.TC_NO;
            //student.Email = model.Email;
            //student.Address = model.Address;
            //student.StudentName = model.StudentName;
            //student.StudentSurname = model.StudentSurname;
            //student.StudentNo = model.StudentNo;
            //student.StudentGSMNumber = model.StudentGSMNumber;
            //student.GPA = model.GPA;
            //student.DepartmentName = model.DepartmentName;
            //student.ProgramName = model.ProgramName;


            return default;
        }

      

        //public Task<BaseEntity> UpdateStudent(UpdateStudentCommandRequest request)
        //{
           
        //}   
    } 
}
