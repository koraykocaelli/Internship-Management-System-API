using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipManagementSystem.Application.Features.Internship.Commands.UploadInternshipBook
{
    public class UploadInternshipBookCommandRequest : IRequest<UploadInternshipBookCommandResponse>
    {
        public IFormFile File { get; set; }

        [FromForm]
        public Guid InternshipId { get; set; }

    }
}