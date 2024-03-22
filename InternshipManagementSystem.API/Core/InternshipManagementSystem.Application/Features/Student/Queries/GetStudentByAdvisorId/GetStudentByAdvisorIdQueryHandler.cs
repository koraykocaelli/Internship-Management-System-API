using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.ViewModels;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Student
{
    public class GetStudentByAdvisorIdQueryHandler : IRequestHandler<GetStudentByAdvisorIdQueryRequest, GetStudentByAdvisorIdQueryResponse>
    {
        private readonly IStudentReadRepository _studentReadRepository;

        public GetStudentByAdvisorIdQueryHandler(IStudentReadRepository studentReadRepository)
        {
            _studentReadRepository = studentReadRepository;
        }
        public async Task<GetStudentByAdvisorIdQueryResponse> Handle(GetStudentByAdvisorIdQueryRequest request, CancellationToken cancellationToken)
        {
            var students = _studentReadRepository.GetWhere(x => x.AdvisorID == request.AdvisorId).ToList();
            return (new GetStudentByAdvisorIdQueryResponse
            {
                Response = new ResponseModel
                {
                    Data = students,
                    Message = students.Count() > 0 ? "Students found" : "No students found",
                    StatusCode = students.Count() > 0 ? 200 : 404   
                }
            });


        


        }
    }
}
