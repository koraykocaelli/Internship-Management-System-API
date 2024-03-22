using InternshipManagementSystem.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Student
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQueryRequest, GetStudentByIdQueryResponse>
    {
        private readonly IStudentWriteRepository _studentWriteRepository;
        private readonly IStudentReadRepository  _studentReadRepository;


        public GetStudentByIdQueryHandler(IStudentReadRepository studentRepository, IStudentWriteRepository studentWriteRepository)
        {
            _studentReadRepository = studentRepository;
            _studentWriteRepository = studentWriteRepository;
        }

        public async Task<GetStudentByIdQueryResponse> Handle(GetStudentByIdQueryRequest request, CancellationToken cancellationToken )
        {
            var student=  await _studentReadRepository.Table.FirstOrDefaultAsync(x => x.ID == request.Id); 
            if (student == null)
            {
                return new GetStudentByIdQueryResponse { Response=new()
                {
                    Data = null,
                    Message = "Student not found",
                    IsSuccess = false,
                    StatusCode = 404
                }
                };
            }
            else
            {
                return new GetStudentByIdQueryResponse {Message="OK", Response = new()
                {
                    Data = student,
                    Message = "Student found",
                    IsSuccess = true,
                    StatusCode = 200
                }
                };
            }


        }

    }
}
