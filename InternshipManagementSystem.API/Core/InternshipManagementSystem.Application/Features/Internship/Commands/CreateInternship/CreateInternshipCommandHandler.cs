using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.Services;
using InternshipManagementSystem.Application.ViewModels;
using InternshipManagementSystem.Domain.Entities;
using MediatR;

namespace InternshipManagementSystem.Application.Features.Internship.Commands.CreateInternship
{
    public class CreateInternshipCommandHandler : IRequestHandler<CreateInternshipCommandRequest, CreateInternshipCommandResponse>
    {
        readonly IStudentReadRepository _studentReadRepository;
        readonly IAdvisorReadRepository _advisorReadRepository;
        readonly IInternshipWriteRepository _internshipWriteRepository;
        readonly IInternshipReadRepository _internshipReadRepository;
        readonly IInternshipApplicationExcelFormReadRepository _internshipApplicationExcelFormReadRepository;
        readonly IInternshipApplicationFormReadRepository _internshipApplicationFormReadRepository;
        readonly IInternshipApplicationFormReadRepository _internshipApplicationFormWriteRepository;
        readonly IInternshipApplicationExcelFormWriteRepository _internshipApplicationExcelFormWriteRepository;
        readonly IMediator _mediator;
        readonly IFileService _fileService;


        public CreateInternshipCommandHandler(IStudentReadRepository studentReadRepository, IAdvisorReadRepository advisorReadRepository, IInternshipWriteRepository internshipWriteRepository, IInternshipApplicationExcelFormReadRepository internshipApplicationExcelFormReadRepository, IInternshipApplicationFormReadRepository internshipApplicationFormReadRepository, IInternshipApplicationFormReadRepository internshipApplicationFormWriteRepository, IInternshipApplicationExcelFormWriteRepository internshipApplicationExcelFormWriteRepository, IMediator mediator, IInternshipReadRepository internshipReadRepository, IFileService fileService)
        {
            _studentReadRepository = studentReadRepository;
            _advisorReadRepository = advisorReadRepository;
            _internshipWriteRepository = internshipWriteRepository;
            _internshipApplicationExcelFormReadRepository = internshipApplicationExcelFormReadRepository;
            _internshipApplicationFormReadRepository = internshipApplicationFormReadRepository;
            _internshipApplicationFormWriteRepository = internshipApplicationFormWriteRepository;
            _internshipApplicationExcelFormWriteRepository = internshipApplicationExcelFormWriteRepository;
            _mediator = mediator;
            _internshipReadRepository = internshipReadRepository;
            _fileService = fileService;
        }

        public async Task<CreateInternshipCommandResponse> Handle(CreateInternshipCommandRequest request, CancellationToken cancellationToken)
        {

            if (await _studentReadRepository.AnyAsync(x => x.StudentNo == request.StudentNo))
            {
            }
            else
            {
                return new CreateInternshipCommandResponse
                {

                    Response = new ResponseModel
                    {
                        Message = "Student not found",
                        StatusCode = 404
                    }
                };

            }
            try
            {
                var internship = new Domain.Entities.Internship
                {
                    AdvisorID = request.AdvisorID,
                    StudentID = request.StudentID,
                    StudentNo = request.StudentNo
                };

                var result = await _internshipWriteRepository.AddAsync(internship);
                //await _internshipWriteRepository.SaveAsync();


                var excel = new InternshipApplicationExcelForm
                {
                    InternshipID = internship.ID,
                    StudentNo = request.StudentNo,
                    FullName = request.FullName,
                    TC_NO = request.TC_NO,
                    InternshipStartDate = request.InternshipStartDate,
                    InternshipEndDate = request.InternshipEndDate,
                    Department = request.Department,
                    StudentGSMNumber = request.StudentGSMNumber,
                    CompanyName = request.CompanyName,
                    NumberOfEmployees = request.NumberOfEmployees,
                    CompanyPhone = request.CompanyPhone,
                    CompanyAddress = request.CompanyAddress,
                    RequestedGovernmentAidAmount = request.RequestedGovernmentAidAmount,
                    ReceivesSalary = request.ReceivesSalary,
                    DoesNotReceiveSalary = request.DoesNotReceiveSalary,
                    Gender = request.Gender,
                    Age = request.Age,
                    ReceivesHealthInsurance = request.ReceivesHealthInsurance,
                    EmailSendingDate = request.EmailSendingDate,
                    Level = request.Level,
                    BirthDate = request.BirthDate
                   

                };
                excel.InternshipID = internship.ID;
                var resultExcel = await _internshipApplicationExcelFormWriteRepository.AddAsync(excel);

                //await _internshipApplicationExcelFormWriteRepository.SaveAsync();
                internship.InternshipApplicationExelFormID = excel.ID;
                //await _internshipWriteRepository.SaveAsync();

                //
                bool fileResult = await _fileService.UploadAsync(internship.ID, request.formFile, filetypes.InternshipApplicationForm);
                await _internshipApplicationExcelFormWriteRepository.SaveAsync();
                await _internshipWriteRepository.SaveAsync();


                return new CreateInternshipCommandResponse()
                {
                    Response = new ResponseModel
                    {
                        Data = internship,
                        Message = "Internship created",
                        StatusCode = 200
                    }


                };
            }
            catch (Exception ex)
            {

                return new CreateInternshipCommandResponse
                {
                    Response = new ResponseModel
                    {
                        Message = ex.Message,
                        StatusCode = 400
                    }
                };
            }

        }
    }
}
