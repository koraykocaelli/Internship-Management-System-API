using Azure.Core;
using InternshipManagementSystem.Application.Features.AppUser.Commands.LoginUser;
using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.Services;
using InternshipManagementSystem.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace InternshipManagementSystem.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _usermanager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signinmanager;

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
        private readonly IMediator _mediator;
        private readonly IInternshipApplicationFormReadRepository _internshipApplicationFormReadRepository;
        private readonly IInternshipApplicationFormWriteRepository _internshipApplicationFormWriteRepository;
        private readonly IInternshipBookReadRepository _internshipBookReadRepository;
        private readonly IInternshipBookWriteRepository _internshipBookWriteRepository;

        public TestController(UserManager<AppUser> usermanager, SignInManager<AppUser> signinmanager, IStudentReadRepository studentReadRepository, IStudentWriteRepository studentWriteRepository, IAdvisorReadRepository advisorReadRepository, IAdvisorWriteRepository advisorWriteRepository, IInternshipReadRepository internshipReadRepository, IInternshipWriteRepository internshipWriteRepository, IInternshipDocumentReadRepository internshipDocumentReadRepository, IInternshipDocumentWriteRepository internshipDocumentWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IMediator mediator, IInternshipApplicationFormReadRepository internshipApplicationFormReadRepository, IInternshipApplicationFormWriteRepository internshipApplicationFormWriteRepository, IInternshipBookReadRepository internshipBookReadRepository, IInternshipBookWriteRepository internshipBookWriteRepository)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
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
            _internshipApplicationFormReadRepository = internshipApplicationFormReadRepository;
            _internshipApplicationFormWriteRepository = internshipApplicationFormWriteRepository;
            _internshipBookReadRepository = internshipBookReadRepository;
            _internshipBookWriteRepository = internshipBookWriteRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Test()
        {
            return Ok("Test");
        }

        [HttpGet("health")]
        public async Task<IActionResult> Health()
        {
            string user = "Anonymous";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = HttpContext.User.Identity.Name;
            }
            var envConnectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_DEFAULT");
            string currentDirectory = Directory.GetCurrentDirectory();



            return Ok($"Connection string health:{envConnectionString != null}, DB connection healthy:{_studentReadRepository != null} Logged in username is:{user} " +
                $"Directory : {currentDirectory}");
        }



        [HttpGet("[action]")]
        public async Task<IActionResult> DownloadBook(Guid InternshipId)
        {
            var x = await _internshipReadRepository.GetByIdAsync(InternshipId);
            if (x == null)
            {
                return NotFound();
            }

            var filename = await _internshipDocumentReadRepository.GetByIdAsync(Guid.Parse(x.InternshipBookID.ToString()));
            string directoryPath = await _fileService.GetPath(InternshipId);
            string filePath = Path.Combine(directoryPath, filename.FileName);

            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/octet-stream", filename.FileName);
            }
            else
            {

                return Ok(filePath);
            }
        }




        // Kullanıcı kaydı ve girişi işlemi
        [HttpPost("register-login")]
        public async Task<IActionResult> RegisterLogin([FromBody] RegisterLoginViewModel request)
        {
            // Kullanıcıyı kullanıcı adı ile bul
            var user = await _usermanager.FindByNameAsync(request.UserName);

            if (user != null)
            {
                //var signInResult = await _signinmanager.PasswordSignInAsync(request.UserName, request.Password, false, false);

                var result = await _usermanager.CheckPasswordAsync(user, request.Password);
                if (result)
                {
                   var data = _signinmanager.SignInAsync(user, true);
                    var role = await _usermanager.GetRolesAsync(user);
                    return Ok(new { Message = "Login successful" 
                    ,
                        data.Id,
                        role
                    });



                };
            }
            else
            {
                // Kullanıcı kayıtlı değil, kullanıcı oluştur ve giriş yap
                var newUser = new AppUser { UserName = request.UserName };
                var createResult = await _usermanager.CreateAsync(newUser, request.Password);

                if (createResult.Succeeded)
                {
                    // Kullanıcı başarıyla oluşturuldu, giriş yap
                    await _signinmanager.SignInAsync(newUser, isPersistent: false);

                    return Ok(new { Message = "Registration and Login successful" });
                }
                else
                {
                    // Hata durumunda sonuçları döndür
                    return BadRequest(new { Errors = createResult.Errors.Select(e => e.Description) });
                }
            }
            return default;
        }
    }

    public class RegisterLoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }



}

