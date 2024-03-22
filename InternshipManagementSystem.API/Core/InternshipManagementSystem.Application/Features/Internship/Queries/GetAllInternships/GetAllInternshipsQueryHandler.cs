using InternshipManagementSystem.Application.Repositories;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship
{
    public class GetAllInternshipsQueryHandler : IRequestHandler<GetAllInternshipsQuery, GetAllInternshipsQueryResponse>
    {
        IInternshipReadRepository _internshipReadRepository;

        public GetAllInternshipsQueryHandler(IInternshipReadRepository internshipReadRepository)
        {
            _internshipReadRepository = internshipReadRepository;
        }

        public Task<GetAllInternshipsQueryResponse> Handle(GetAllInternshipsQuery request, CancellationToken cancellationToken)
        {
            var interships = _internshipReadRepository.GetAll().ToList();
            if (interships == null || interships.Count == 0)
            {
                return Task.FromResult(new GetAllInternshipsQueryResponse
                {
                    Response = new()
                    {
                        Data = null,
                        Message = "No interships found",
                        IsSuccess = false,
                        StatusCode = 404
                    }
                });
            }
            else
            {
                return Task.FromResult(new GetAllInternshipsQueryResponse
                {
                    Response = new()
                    {
                        Data = interships,
                        Message = "Interships found",
                        IsSuccess = true,
                        StatusCode = 200
                    }
                });

            }
        }
    }
}
