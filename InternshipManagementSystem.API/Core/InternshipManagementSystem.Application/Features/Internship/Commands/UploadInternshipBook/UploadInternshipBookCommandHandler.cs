using InternshipManagementSystem.Application.Services;
using InternshipManagementSystem.Application.ViewModels;
using InternshipManagementSystem.Domain.Entities;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Commands.UploadInternshipBook
{
    
    public class UploadInternshipBookCommandHandler : IRequestHandler<UploadInternshipBookCommandRequest, UploadInternshipBookCommandResponse>
    {
        private readonly IFileService _fileService;

        public UploadInternshipBookCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<UploadInternshipBookCommandResponse> Handle(UploadInternshipBookCommandRequest request, CancellationToken cancellationToken)
        {

            try
            {

                ////var file = files.FirstOrDefault();

                var data = await _fileService.UploadAsync(request.InternshipId, request.File, filetypes.InternshipBook);

                return new UploadInternshipBookCommandResponse()
                {
                    ResponseModel = new ResponseModel()
                    {
                        Data = data,
                        Message = "Internship Book Uploaded Successfully",
                        StatusCode = 200,
                        IsSuccess = true
                    }

                };
            }
            catch (Exception ex)
            {

                return new UploadInternshipBookCommandResponse()
                {
                    ResponseModel = new ResponseModel()
                    {
                        Data = null,
                        Message = ex.Message,
                        StatusCode = 500,
                        IsSuccess = false
                    }

                };
            }


            throw new NotImplementedException();
        }
    }
}
