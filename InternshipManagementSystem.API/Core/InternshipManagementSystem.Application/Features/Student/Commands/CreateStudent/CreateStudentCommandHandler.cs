using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Student.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommandRequest, CreateStudentCommandResponse>
    {
        readonly IStudentReadRepository _studentReadRepository;
        readonly IStudentWriteRepository _studentWriteRepository;

        public CreateStudentCommandHandler(IStudentReadRepository studentReadRepository, IStudentWriteRepository studentWriteRepository)
        {
            _studentReadRepository = studentReadRepository;
            _studentWriteRepository = studentWriteRepository;
        }

        public async Task<CreateStudentCommandResponse> Handle(CreateStudentCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Student e = _studentReadRepository.GetFirst(x => x.TC_NO == request.TC_NO || x.StudentNo == request.StudentNo, false);
            if (e is not null)
            {
                var str = e.StudentNo == request.StudentNo ? "Student Number" : "TC Number";

                return new CreateStudentCommandResponse
                {
                    Response = new ResponseModel
                    {
                        IsSuccess = false,
                        Message = $"There is a student with same {str} number",
                        Data = null,
                        StatusCode = 400
                    }
                   

                };
            }
            Domain.Entities.Student student = new()
            {
                Address = request.Address,
                Email = request.Email,
                GPA = request.GPA,
                StudentGSMNumber = request.StudentGSMNumber,
                StudentName = request.StudentName,
                StudentNo = request.StudentNo,
                StudentSurname = request.StudentSurname,
                TC_NO = request.TC_NO,
                DepartmentName = request.DepartmentName,
                ProgramName = request.ProgramName,
                FacultyName = request.FacultyName,
            };

            bool progres = await _studentWriteRepository.AddAsync(student);
            await _studentWriteRepository.SaveAsync();

            if (progres == true)
            {
                
                return new CreateStudentCommandResponse
                {
                    Response = new ResponseModel
                    {
                        IsSuccess = true,
                        Message = "Student added",
                        Data = student,
                        StatusCode = 200
                    }


                };

            }
            else
            {
                return new CreateStudentCommandResponse
                {
                    Response = new ResponseModel
                    {
                        IsSuccess = false,
                        Message = "Some problems",
                        Data = null,
                        StatusCode = 400
                    }


                };
            }
        }
    }
}
