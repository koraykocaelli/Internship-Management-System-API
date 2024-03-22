using InternshipManagementSystem.Application.Features.AppUser.Commands.CreateUser;
using InternshipManagementSystem.Application.Features.AppUser.Commands.LoginUser;
using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternshipManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IStudentReadRepository _studentReadRepository;
        private readonly IStudentWriteRepository _studentWriteRepository;
        private readonly IAdvisorReadRepository _advisorReadRepository;
        private readonly IAdvisorWriteRepository _advisorWriteRepository;
        private readonly IInternshipReadRepository _internshipReadRepository;
        private readonly IInternshipWriteRepository _internshipWriteRepository;
        private readonly IInternshipDocumentReadRepository _internshipDocumentReadRepository;
        private readonly IInternshipDocumentWriteRepository _internshipDocumentWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        readonly IMediator _mediator;
        public UsersController(IStudentReadRepository studentReadRepository, IStudentWriteRepository studentWriteRepository, IAdvisorReadRepository advisorReadRepository, IAdvisorWriteRepository advisorWriteRepository, IInternshipReadRepository internshipReadRepository, IInternshipWriteRepository internshipWriteRepository, IInternshipDocumentReadRepository internshipDocumentReadRepository, IInternshipDocumentWriteRepository internshipDocumentWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IMediator mediator)
        {
            _studentReadRepository = studentReadRepository;
            _studentWriteRepository = studentWriteRepository;
            _advisorReadRepository = advisorReadRepository;
            _advisorWriteRepository = advisorWriteRepository;
            _internshipReadRepository = internshipReadRepository;
            _internshipWriteRepository = internshipWriteRepository;
            _internshipDocumentReadRepository = internshipDocumentReadRepository;
            _internshipDocumentWriteRepository = internshipDocumentWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _mediator = mediator;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest   createUserCommandRequest)
        {
            CreateUserCommandRequestResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

       
    }
}
