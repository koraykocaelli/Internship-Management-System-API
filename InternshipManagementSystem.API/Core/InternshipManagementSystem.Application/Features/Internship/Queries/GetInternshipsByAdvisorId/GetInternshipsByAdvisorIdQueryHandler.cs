using InternshipManagementSystem.Application.Repositories;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship
{
    public class GetInternshipsByAdvisorIdQueryHandler : IRequestHandler<GetInternshipsByAdvisorIdQueryRequest, GetInternshipsByAdvisorIdQueryResponse>
    {
        IInternshipReadRepository _internshipReadRepository;

        public GetInternshipsByAdvisorIdQueryHandler(IInternshipReadRepository internshipReadRepository)
        {
            _internshipReadRepository = internshipReadRepository;
        }

        public Task<GetInternshipsByAdvisorIdQueryResponse> Handle(GetInternshipsByAdvisorIdQueryRequest request, CancellationToken cancellationToken)
        {
            var interships = _internshipReadRepository.GetWhere(i => i.AdvisorID == request.AdvisorId).ToList();
            if (interships == null || interships.Count == 0)
            {
                return Task.FromResult(new GetInternshipsByAdvisorIdQueryResponse
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
                return Task.FromResult(new GetInternshipsByAdvisorIdQueryResponse
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
