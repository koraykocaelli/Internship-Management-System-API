using InternshipManagementSystem.Application.Repositories;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship
{
    public class GetInternshipsByStudentIdQueryHandler : IRequestHandler<GetInternshipsByStudentIdQueryRequest, GetInternshipsByStudentIdQueryResponse>
    {
        IInternshipReadRepository _internshipReadRepository;

        public GetInternshipsByStudentIdQueryHandler(IInternshipReadRepository internshipReadRepository)
        {
            _internshipReadRepository = internshipReadRepository;
        }



        public Task<GetInternshipsByStudentIdQueryResponse> Handle(GetInternshipsByStudentIdQueryRequest request, CancellationToken cancellationToken)
        {
            var interships = _internshipReadRepository.GetWhere(i => i.StudentID == request.StudentId).ToList();
            if (interships == null || interships.Count == 0)
            {
                return Task.FromResult(new GetInternshipsByStudentIdQueryResponse
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
                return Task.FromResult(new GetInternshipsByStudentIdQueryResponse
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
