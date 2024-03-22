using InternshipManagementSystem.Application.Repositories;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Commands
{
    public class UpdateInternshipCommandHandler : IRequestHandler<UpdateInternshipCommandRequest, UpdateInternshipCommandResponse>
    {
        private readonly IInternshipReadRepository _internshipReadRepository;
        private readonly IInternshipWriteRepository _internshipWriteRepository;


        public UpdateInternshipCommandHandler(IInternshipReadRepository internshipReadRepository, IInternshipWriteRepository internshipWriteRepository)
        {
            _internshipReadRepository = internshipReadRepository;
            _internshipWriteRepository = internshipWriteRepository;
        }

        public async Task<UpdateInternshipCommandResponse> Handle(UpdateInternshipCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await _internshipReadRepository.GetByIdAsync(request.InternshipID);
            if (data is not null)
            {
                try
                {
                    data.StudentName = request.StudentName;
                    data.StudentSurname = request.StudentSurname;
                    data.StudentNo = request.StudentNo;
                    data.InternshipApplicationFormID = request.InternshipApplicationFormID;
                    data.InternshipApplicationExelFormID = request.InternshipApplicationExelFormID;
                    data.InternshipBookID = request.InternshipBookID;
                    data.SPASID = request.SPASID;
                    await _internshipWriteRepository.SaveAsync();
                    return new UpdateInternshipCommandResponse
                    {
                        Response = new()
                        {
                            Data = data,
                            Message = "Internship updated",
                            IsSuccess = true,
                            StatusCode = 200
                        }
                    };
                }
                catch (Exception ex)
                {
                    return new UpdateInternshipCommandResponse
                    {
                        Response = new()
                        {
                            Data = null,
                            Message = ex.Message,
                            IsSuccess = false,
                            StatusCode = 400
                        }
                    };
                }
            }
            else
            {
                return new UpdateInternshipCommandResponse
                {
                    Response = new()
                    {
                        Data = null,
                        Message = "Internship not found",
                        IsSuccess = false,
                        StatusCode = 404
                    }
                };

            }
        }
    }
}
