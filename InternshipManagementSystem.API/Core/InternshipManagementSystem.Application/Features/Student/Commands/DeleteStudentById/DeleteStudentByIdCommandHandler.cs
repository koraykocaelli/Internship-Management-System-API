using InternshipManagementSystem.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Student
{
    public class DeleteStudentByIdCommandHandler : IRequestHandler<DeleteStudentByIdCommandRequest, DeleteStudentByIdCommandResponse>
    {
        private readonly IStudentWriteRepository _studentWriteRepository;
        private readonly IStudentReadRepository _studentReadRepository;

        public DeleteStudentByIdCommandHandler(IStudentWriteRepository studentWriteRepository, IStudentReadRepository studentReadRepository)
        {
            _studentWriteRepository = studentWriteRepository;
            _studentReadRepository = studentReadRepository;
        }

        public async Task<DeleteStudentByIdCommandResponse> Handle(DeleteStudentByIdCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _studentWriteRepository.RemoveAsync(request.Id))
                {
                    await _studentWriteRepository.SaveAsync();
                    return new DeleteStudentByIdCommandResponse()
                    {
                        Response = new()
                        {
                            Data = null,
                            Message = "Student deleted",
                            IsSuccess = true,
                            StatusCode = 200
                        }
                    };
                }
                else
                {
                    return new DeleteStudentByIdCommandResponse()
                    {
                        Response = new()
                        {
                            Data = null,
                            Message = "Student not found",
                            IsSuccess = false,
                            StatusCode = 404
                        }
                    };
                }

            }
            catch (Exception ex)
            {

               return new DeleteStudentByIdCommandResponse()
               {
                   Response = new()
                   {
                       Data = null,
                       Message = ex.Message,
                       IsSuccess = false,
                       StatusCode = 500
                   }
               };
            }
        


         
        }
    }
}
