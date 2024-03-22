using InternshipManagementSystem.Application.Features.Student;
using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.Services;
using InternshipManagementSystem.Application.ViewModels;
using InternshipManagementSystem.Application.ViewModels.StudentViewModels;
using InternshipManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternshipManagementSystem.Application.Features;
using InternshipManagementSystem.Application.Features.Student.Commands.CreateStudent;
using InternshipManagementSystem.Application.Features.Student.Commands.AddStudentToAdvisor;
using Microsoft.AspNetCore.Authorization;

namespace InternshipManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentReadRepository _studentReadRepository;
        private readonly IStudentWriteRepository _studentWriteRepository;
        private readonly IAdvisorReadRepository _advisorReadRepository;
        private readonly IAdvisorWriteRepository _advisorWriteRepository;
        private readonly IMediator _mediator;

        public StudentController(IStudentReadRepository studentReadRepository, IStudentWriteRepository studentWriteRepository, IAdvisorReadRepository advisorReadRepository, IAdvisorWriteRepository advisorWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IMediator mediator)
        {
            _studentReadRepository = studentReadRepository;
            _studentWriteRepository = studentWriteRepository;
            _advisorReadRepository = advisorReadRepository;
            _advisorWriteRepository = advisorWriteRepository;
            _mediator = mediator;
        }
        [Authorize("Student")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetStudentAllQueryResponse response = await _mediator.Send(new GetStudentAllQueryRequest());

            return Ok(response.Response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudentById([FromQuery] GetStudentByIdQueryRequest request)
        {
            GetStudentByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response.Response);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudentByUsername([FromQuery] GetStudentByUsernameQueryRequest request)
        {
            GetStudentByUsernameQueryResponse response = await _mediator.Send(request);
            return Ok(response.Response);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudentByAdvisorId([FromQuery] GetStudentByAdvisorIdQueryRequest request)
        {
            GetStudentByAdvisorIdQueryResponse response = await _mediator.Send(request);
            return Ok(response.Response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddToAdvisor(AddStudentToAdvisorRequest request)
        {
            AddStudentToAdvisorResponse response = await _mediator.Send(request);
            return Ok(response.Response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateStudentCommandRequest request)
        {
           
            CreateStudentCommandResponse response= await _mediator.Send(request);
            return Ok(response.Response);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentCommandRequest request)
        {
            var student = await _studentReadRepository.GetByIdAsync(request.StudentID);

            if (student is not null)
            {
                UpdateStudentCommandResponse response = await _mediator.Send(request);
            }


            await _studentWriteRepository.SaveAsync();
            return Ok(
               new ResponseModel(true, "Succesful", student, 200)
               );
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteStudentByIdCommandRequest request)
        {
            DeleteStudentByIdCommandResponse response = await _mediator.Send(request);
            return Ok(response.Response);
        }



    }
}
