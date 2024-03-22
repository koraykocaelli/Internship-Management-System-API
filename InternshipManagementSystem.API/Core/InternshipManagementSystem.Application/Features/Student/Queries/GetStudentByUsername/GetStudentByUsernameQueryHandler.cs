using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Student { 

    public class GetStudentByUsernameQueryHandler : IRequestHandler<GetStudentByUsernameQueryRequest, GetStudentByUsernameQueryResponse>
    {
        IStudentReadRepository _studentReadRepository;
        public GetStudentByUsernameQueryHandler(IStudentReadRepository studentReadRepository)
        {
            _studentReadRepository = studentReadRepository;
        }

        public async Task<GetStudentByUsernameQueryResponse> Handle(GetStudentByUsernameQueryRequest request, CancellationToken cancellationToken)
        {
            var student = await _studentReadRepository.GetSingleAsync(x => x.StudentNo == request.Username);
            if (student == null)
            {
                return new GetStudentByUsernameQueryResponse
                {
                    Response = new()
                    {
                        Data = null,
                        Message = "No student found",
                        IsSuccess = false,
                        StatusCode = 404
                    }
                };
            }
            else
            {
                return new GetStudentByUsernameQueryResponse
                {
                    Message = "OK",
                    Response = new()
                    {
                        Data = student,
                        Message = "Students found",
                        IsSuccess = true,
                        StatusCode = 200
                    }
                };
            }

        }

    }
}
