using InternshipManagementSystem.Application.Repositories;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Commands
{
    public class UpdateInternshipByInternshipStatus : IRequestHandler<UpdateInternshipByInternshipStatusCommandRequest, UpdateInternshipByInternshipStatusCommandResponse>
    {
        private readonly IInternshipReadRepository _internshipReadRepository;
        private readonly IInternshipWriteRepository _internshipWriteRepository;

        public UpdateInternshipByInternshipStatus(IInternshipReadRepository internshipReadRepository, IInternshipWriteRepository internshipWriteRepository)
        {
            _internshipReadRepository = internshipReadRepository;
            _internshipWriteRepository = internshipWriteRepository;
        }

        public async Task<UpdateInternshipByInternshipStatusCommandResponse> Handle(UpdateInternshipByInternshipStatusCommandRequest request, CancellationToken cancellationToken)
        {
          var internship =await  _internshipReadRepository.GetByIdAsync(request.InternshipID);
            if(internship == null)
            {
                return new UpdateInternshipByInternshipStatusCommandResponse
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
            else
            {
                internship.InternshipStatus = request.Status;
                await _internshipWriteRepository.SaveAsync();
                return new UpdateInternshipByInternshipStatusCommandResponse
                {
                    Response = new()
                    {
                        Data = internship,
                        Message = "Internship updated",
                        IsSuccess = true,
                        StatusCode = 200
                    }
                };
            }
            throw new NotImplementedException();
        }
    }
}
