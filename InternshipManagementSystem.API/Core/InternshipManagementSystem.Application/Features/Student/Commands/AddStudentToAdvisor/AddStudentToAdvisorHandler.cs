using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Student.Commands.AddStudentToAdvisor
{
    public class AddStudentToAdvisorHandler : IRequestHandler<AddStudentToAdvisorRequest, AddStudentToAdvisorResponse>
    { 
        readonly IStudentWriteRepository _studentWriteRepository;
        readonly IStudentReadRepository _studentReadRepository;
        readonly IAdvisorReadRepository _advisorReadRepository;

        public AddStudentToAdvisorHandler(IStudentWriteRepository studentWriteRepository, IStudentReadRepository studentReadRepository, IAdvisorReadRepository advisorReadRepository)
        {
            _studentWriteRepository = studentWriteRepository;
            _studentReadRepository = studentReadRepository;
            _advisorReadRepository = advisorReadRepository;
        }

        public async Task<AddStudentToAdvisorResponse> Handle(AddStudentToAdvisorRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Student student = await _studentReadRepository.GetSingleAsync(s => s.ID == request.StudentID, true);
            if (student is null)
            {
                return new AddStudentToAdvisorResponse
                {
                    Response = new ResponseModel
                    {
                        IsSuccess = false,
                        Message = $"There is not a student with these properties",
                        Data = null,
                        StatusCode = 400
                    }
                   

                };

            }

            var advisor = await _advisorReadRepository.GetAll().Include(a => a.Students).FirstOrDefaultAsync(x => x.ID == request.AdvisorID);
            var ifStudentExists = advisor.Students.Any(student => student.ID == request.StudentID);
            if (ifStudentExists)
            {
                return new AddStudentToAdvisorResponse
                {
                    Response= new ResponseModel
                    {
                        IsSuccess = false,
                        Message = "Student already exists",
                        Data = null,
                        StatusCode = 400
                    }
                   

                };
            }
            if (advisor != null)
            {
                try
                {
                    student.AdvisorID = advisor.ID;

                    var y = await _studentWriteRepository.SaveAsync();
                    
                    return new AddStudentToAdvisorResponse
                    {
                        Response = new ResponseModel
                        {
                            IsSuccess = true,
                            Message = "Successful",
                            Data = student,
                            StatusCode = 200
                        }
                    };


                }
                catch (Exception ex)
                {
                    return new AddStudentToAdvisorResponse
                    {
                        Response= new ResponseModel
                        {
                            IsSuccess = false,
                            Message = ex.Message,
                            Data = null,
                            StatusCode = 400
                        }
                       
                    };
                }

            }
            return new AddStudentToAdvisorResponse
            {
                Response= new ResponseModel {
                    IsSuccess = false,
                    Message = "Some problems",
                    Data = null,
                    StatusCode = 499
                }
                
            };

        }
    }
}
