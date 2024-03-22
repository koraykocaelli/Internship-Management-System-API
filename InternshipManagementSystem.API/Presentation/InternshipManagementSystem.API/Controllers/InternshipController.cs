using InternshipManagementSystem.Application.Features.Internship;
using InternshipManagementSystem.Application.Features.Internship.Commands;
using InternshipManagementSystem.Application.Features.Internship.Commands.CreateInternship;
using InternshipManagementSystem.Application.Features.Internship.Commands.DeleteInternship;
using InternshipManagementSystem.Application.Features.Internship.Commands.UploadInternshipBook;
using InternshipManagementSystem.Application.Features.Internship.Queries.GetInternshipByInternshipId;
using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.Services;
using InternshipManagementSystem.Application.ViewModels;
using InternshipManagementSystem.Application.ViewModels.InternshipViewModelss;
using InternshipManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InternshipManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class InternshipController : ControllerBase
    {

        private readonly IInternshipReadRepository _internshipReadRepository;
        private readonly IInternshipDocumentReadRepository _internshipDocumentReadRepository;
        private readonly IInternshipDocumentWriteRepository _internshipDocumentWriteRepository;
        private readonly IFileService _fileService;
        private readonly IMediator _mediator;
        private readonly IInternshipApplicationFormReadRepository _internshipApplicationFormReadRepository;
        private readonly IInternshipBookReadRepository _internshipBookReadRepository;
        private readonly IInternshipBookWriteRepository _internshipBookWriteRepository;
        private readonly IInternshipBookWriteRepository _internshipApplicationFormWriteRepository;

        public InternshipController(IInternshipReadRepository internshipReadRepository, IInternshipDocumentReadRepository internshipDocumentReadRepository, IInternshipDocumentWriteRepository internshipDocumentWriteRepository, IFileService fileService, IMediator mediator, IInternshipApplicationFormReadRepository internshipApplicationFormReadRepository, IInternshipBookReadRepository internshipBookReadRepository, IInternshipBookWriteRepository internshipBookWriteRepository, IInternshipBookWriteRepository internshipApplicationFormWriteRepository)
        {
            _internshipReadRepository = internshipReadRepository;
            _internshipDocumentReadRepository = internshipDocumentReadRepository;
            _internshipDocumentWriteRepository = internshipDocumentWriteRepository;
            _fileService = fileService;
            _mediator = mediator;
            _internshipApplicationFormReadRepository = internshipApplicationFormReadRepository;
            _internshipBookReadRepository = internshipBookReadRepository;
            _internshipBookWriteRepository = internshipBookWriteRepository;
            _internshipApplicationFormWriteRepository = internshipApplicationFormWriteRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllInternships()
        {
            GetAllInternshipsQuery query = new GetAllInternshipsQuery();
            GetAllInternshipsQueryResponse response = await _mediator.Send(query);
            return Ok(response.Response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInternshipByInternshipId([FromQuery] GetInternshipByInternshipIdQueryRequest request)
        {
            GetInternshipByInternshipIdQueryResponse response = await _mediator.Send(request);
            return Ok();

        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetIntershipsByStudentId([FromQuery] GetInternshipsByStudentIdQueryRequest request)
        {
            GetInternshipsByStudentIdQueryResponse response = await _mediator.Send(request);
            return Ok(response.Response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetIntershipsAdvisorId([FromQuery] GetInternshipsByAdvisorIdQueryRequest request)
        {
            GetInternshipsByAdvisorIdQueryResponse response = await _mediator.Send(request);
            return Ok(response.Response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateInternship([FromForm] CreateInternshipCommandRequest request)
        {
            CreateInternshipCommandResponse response = await _mediator.Send(request);
            return Ok(response.Response);


        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateInternship(UpdateInternshipCommandRequest request)
        {
            UpdateInternshipCommandResponse response = await _mediator.Send(request);
            return Ok(response.Response);

        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateInternshipByInternshipStatus(UpdateInternshipByInternshipStatusCommandRequest request)
        {
            UpdateInternshipByInternshipStatusCommandResponse response = await _mediator.Send(request);
            return Ok(response.Response);
        }

        //
        //
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteInternship(DeleteInternshipCommandRequest request)
        {
            DeleteInternshipCommandResponse response = await _mediator.Send(request);
            return Ok(response.Response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllForms(Guid internshipId)
        {
            var documents = _internshipApplicationFormReadRepository.GetWhere(x => x.InternshipID == internshipId).ToList();


            return Ok(new ResponseModel()
            {
                Data = documents,
                IsSuccess = documents == null || documents.Count == 0 ? false : true,
                Message = documents == null || documents.Count == 0 ? "No Document Found" : "Documents Found",
                StatusCode = documents == null || documents.Count == 0 ? 404 : 200
            });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBooks(Guid internshipId)
        {
            var documents = _internshipBookReadRepository.GetWhere(x => x.InternshipID == internshipId).ToList();


            return Ok(new ResponseModel()
            {
                Data = documents,
                IsSuccess = documents == null || documents.Count == 0 ? false : true,
                Message = documents == null || documents.Count == 0 ? "No Document Found" : "Documents Found",
                StatusCode = documents == null || documents.Count == 0 ? 404 : 200
            });
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteBookByInternshipId(Guid internshipId)
        {
            var book =  _internshipBookReadRepository.Table.Where(x => x.InternshipID == internshipId).FirstOrDefault();
            var data = new object();

            if (book is not null)
            {
                data = book;
                if (await _fileService.DeleteFileAsync(book.FilePath) == false)
                {
                    var test = await _internshipBookWriteRepository.RemoveAsync(book.ID);
                    await _internshipBookWriteRepository.SaveAsync();
                    return Ok(new ResponseModel()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "File Not Deleted",
                        StatusCode = 404
                    });
                }
                else
                {
                    var test = await _internshipBookWriteRepository.RemoveAsync(book.ID);
                    await _internshipBookWriteRepository.SaveAsync();
                    return Ok(new ResponseModel()
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = "File Deleted",
                        StatusCode = 200
                    });
                }


            }

            return BadRequest(new ResponseModel()
            {
                Data = data,
                IsSuccess = true,
                Message = "File Could not be Deleted",
                StatusCode = 200
            
            });
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteFormByInternshipId(Guid id)
        {
            var book = await _internshipBookReadRepository.GetByIdAsync(id);
            var form = await _internshipApplicationFormReadRepository.GetByIdAsync(id);
            var data = new object();


            if (book is not null)
            {
                data = book;
                if (await _fileService.DeleteFileAsync(book.FilePath) == false)
                {
                    var test = await _internshipBookWriteRepository.RemoveAsync(book.ID);
                    await _internshipBookWriteRepository.SaveAsync();
                    return Ok(new ResponseModel()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "File Not Deleted",
                        StatusCode = 404
                    });
                }
                else
                {
                    var test = await _internshipBookWriteRepository.RemoveAsync(book.ID);
                    await _internshipBookWriteRepository.SaveAsync();
                    return Ok(new ResponseModel()
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = "File Deleted",
                        StatusCode = 200
                    });
                }


            }
            else if (form is not null)
            {
                if (await _fileService.DeleteFileAsync(form.FilePath) == false)
                {
                    var test = await _internshipApplicationFormWriteRepository.RemoveAsync(form.ID);
                    await _internshipApplicationFormWriteRepository.SaveAsync();


                    return Ok(new ResponseModel()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "File Not Deleted",
                        StatusCode = 404
                    });
                }
                else
                {
                    var test = await _internshipApplicationFormWriteRepository.RemoveAsync(form.ID);
                    await _internshipApplicationFormWriteRepository.SaveAsync();

                    return Ok(new ResponseModel()
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = "File Deleted",
                        StatusCode = 200
                    });
                }
            }
            return BadRequest(new ResponseModel()
            {
                Data = data,
                IsSuccess = true,
                Message = "File Deleted",
                StatusCode = 200
            });
        }




        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            var book = await _internshipBookReadRepository.GetByIdAsync(id);
            var form = await _internshipApplicationFormReadRepository.GetByIdAsync(id);
            var data = new object();


            if (book is not null)
            {
                data = book;
                if (await _fileService.DeleteFileAsync(book.FilePath) == false)
                {
                    var test = await _internshipBookWriteRepository.RemoveAsync(book.ID);
                    await _internshipBookWriteRepository.SaveAsync();
                    return Ok(new ResponseModel()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "File Not Deleted",
                        StatusCode = 404
                    });
                }
                else
                {
                    var test = await _internshipBookWriteRepository.RemoveAsync(book.ID);
                    await _internshipBookWriteRepository.SaveAsync();
                    return Ok(new ResponseModel()
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = "File Deleted",
                        StatusCode = 200
                    });
                }


            }
            else if (form is not null)
            {
                if (await _fileService.DeleteFileAsync(form.FilePath) == false)
                {
                    var test = await _internshipApplicationFormWriteRepository.RemoveAsync(form.ID);
                    await _internshipApplicationFormWriteRepository.SaveAsync();


                    return Ok(new ResponseModel()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "File Not Deleted",
                        StatusCode = 404
                    });
                }
                else
                {
                    var test = await _internshipApplicationFormWriteRepository.RemoveAsync(form.ID);
                    await _internshipApplicationFormWriteRepository.SaveAsync();

                    return Ok(new ResponseModel()
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = "File Deleted",
                        StatusCode = 200
                    });
                }
            }
            return BadRequest(new ResponseModel()
            {
                Data = data,
                IsSuccess = true,
                Message = "File Deleted",
                StatusCode = 200
            });
        }
        //
        //




        [HttpPost("[action]")]
        public async Task<IActionResult> UploadInternshipBook([FromForm] IFormFileCollection files, [FromForm] Guid internshipId)
        {

            try
            {
                var file = HttpContext.Request.Form.Files.FirstOrDefault();
                UploadInternshipBookCommandRequest request = new UploadInternshipBookCommandRequest()
                {
                    File = file,
                    InternshipId = internshipId
                };
                UploadInternshipBookCommandResponse response = await _mediator.Send(request);
                return Ok(response.ResponseModel);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = 500
                });
            }



        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UploadApplicationForm([FromForm] IFormFileCollection files, [FromForm]Guid internshipId)
        {
            var file = files.FirstOrDefault();
            var data = await _fileService.UploadAsync(internshipId, file, filetypes.InternshipApplicationForm);

            return Ok(new ResponseModel()
            {
                Message = data ? "Successful" : "Unsuccessful",
                Data = data,
                IsSuccess = data ? true : false,
                StatusCode = data ? 200 : 400
            });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInternshipBookIdByInternshipId(Guid internshipId)
        {
            var internship = await _internshipReadRepository.GetByIdAsync(internshipId);
            if (internship == null)
            {
                return NotFound();
            }

            var file = await _internshipBookReadRepository.GetByIdAsync(Guid.Parse(internship.InternshipBookID.ToString()));
            if (file is null)
            {
                return NotFound(
                    new ResponseModel()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "File is not found",
                        StatusCode = 400
                    }
                    ); ;
            }
            else
            {
                return Ok(new ResponseModel()
                {
                    Data = file.ID,
                    IsSuccess = true,
                    Message = "File is found",
                    StatusCode = 200
                });
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetInternshipBookByInternshipId(Guid internshipId)
        {
            var internship = await _internshipReadRepository.GetByIdAsync(internshipId);
            if (internship == null)
            {
                return NotFound();
            }

            var file = await _internshipBookReadRepository.GetByIdAsync(internshipId);
            if (file is null)
            {
                return NotFound(
                    new ResponseModel()
                    {
                        Data = string.Empty,
                        IsSuccess = false,
                        Message = "File is not found",
                        StatusCode = 400
                    }
                    );
            }
            string directoryPath = await _fileService.GetPath(internshipId);

            string filePath = Path.Combine(directoryPath, file.FileName);

            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/octet-stream", file.FileName);
            }
            else
            {
                return Ok(filePath);
            }
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetApplicationFormByInternshipId(Guid internshipId)
        {
            var internship = await _internshipReadRepository.GetByIdAsync(internshipId);
            if (internship == null)
            {
                return NotFound();
            }

            var file = await _internshipApplicationFormReadRepository.GetByIdAsync(Guid.Parse(internship.InternshipApplicationFormID.ToString()));
            string directoryPath = await _fileService.GetPath(internshipId);
            string filePath = Path.Combine(directoryPath, file.FileName);

            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/octet-stream", file.FileName);
            }
            else
            {

                return Ok(filePath);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInternshipExcelForm([FromQuery] GetInternshipExcelFormQueryRequest request)
        {
            GetInternshipExcelFormQueryResponse response = await _mediator.Send(request);
            return Ok(response.Response);

        }
    }

}
