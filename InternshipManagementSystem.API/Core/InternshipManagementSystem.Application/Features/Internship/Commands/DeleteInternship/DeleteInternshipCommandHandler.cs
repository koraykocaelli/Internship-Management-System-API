using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Internship.Commands.DeleteInternship
{
    public class DeleteInternshipCommandHandler : IRequestHandler<DeleteInternshipCommandRequest, DeleteInternshipCommandResponse>
    {
        private readonly IInternshipWriteRepository _internshipWriteRepository;
        private readonly IInternshipReadRepository _internshipReadRepository;

       
        public DeleteInternshipCommandHandler(IInternshipWriteRepository internshipWriteRepository, IInternshipReadRepository internshipReadRepository)
        {
            _internshipWriteRepository = internshipWriteRepository;
            _internshipReadRepository = internshipReadRepository;
        }

        public async Task<DeleteInternshipCommandResponse> Handle(DeleteInternshipCommandRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var internship = await _internshipReadRepository.GetByIdAsync(request.Id);
                if (internship == null)
                {
                    return new DeleteInternshipCommandResponse
                    {
                        Response = new ResponseModel
                        {
                            Data = null,
                            IsSuccess=false,
                            Message = "Internship not found",
                            StatusCode = 404
                        }
                    };
                }
                await _internshipWriteRepository.RemoveAsync(internship.ID);
                return new DeleteInternshipCommandResponse
                {
                    Response = new ResponseModel
                    {
                        Message = "Internship deleted successfully",
                        StatusCode = 200
                    }
                };

            }
            catch (Exception ex)
            {
                return new DeleteInternshipCommandResponse
                {
                    Response = new ResponseModel
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = ex.Message,
                        StatusCode = 500
                    }
                };
                
            }
            
        }
    }
}
