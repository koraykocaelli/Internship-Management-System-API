using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.ViewModels;
using MediatR;


namespace InternshipManagementSystem.Application.Features.Student
{
    public class GetStudentAllQueryHandler : IRequestHandler<GetStudentAllQueryRequest, GetStudentAllQueryResponse>
    {
        IStudentReadRepository _studentReadRepository;
        public GetStudentAllQueryHandler(IStudentReadRepository studentReadRepository)
        {
            _studentReadRepository = studentReadRepository;
        }
        public async Task<GetStudentAllQueryResponse> Handle(GetStudentAllQueryRequest request, CancellationToken cancellationToken)
        {
         var students = _studentReadRepository.Table.ToList();
          
          if (students.Count == 0)
            {
              return new GetStudentAllQueryResponse { Response = new()
              {
                  Data = null,
                  Message = "No student found",
                  IsSuccess = false,
                  StatusCode = 404
              }
              };
          }
          else
            {
              return new GetStudentAllQueryResponse {   Response = new()
              {
                  Data = students,
                  Message = "Students found",
                  IsSuccess = true,
                  StatusCode = 200
              }
              };
          }
               
           
        }

    }
}
