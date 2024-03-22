using InternshipManagementSystem.Application.Repositories;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Queries.GetInternshipByInternshipId
{
    internal class GetInternshipByInternshipIdQueryHandler : IRequestHandler<GetInternshipByInternshipIdQueryRequest, GetInternshipByInternshipIdQueryResponse>
    {
        IInternshipReadRepository _internshipReadRepository;
        public GetInternshipByInternshipIdQueryHandler(IInternshipReadRepository internshipReadRepository)
        {
            _internshipReadRepository = internshipReadRepository;
        }

        public async Task<GetInternshipByInternshipIdQueryResponse> Handle(GetInternshipByInternshipIdQueryRequest request, CancellationToken cancellationToken)
        {


            var internship =  await _internshipReadRepository.GetSingleAsync(i => i.ID == request.InternshipId);
            if (internship == null)
            {
                return new GetInternshipByInternshipIdQueryResponse
                {
                    Response = new()
                    {
                        Data = null,
                        Message = "No internship found",
                        IsSuccess = false,
                        StatusCode = 404
                    }
                };
            }
            else
            {
                return new GetInternshipByInternshipIdQueryResponse
                {
                    Response = new()
                    {
                        Data = internship,
                        Message = "Internship found",
                        IsSuccess = true,
                        StatusCode = 200
                    }
                };

            }


        }
    }
}
