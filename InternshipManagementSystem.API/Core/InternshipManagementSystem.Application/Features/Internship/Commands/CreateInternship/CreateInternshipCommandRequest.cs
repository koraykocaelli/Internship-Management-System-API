using InternshipManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace InternshipManagementSystem.Application.Features.Internship.Commands.CreateInternship
{
    public class CreateInternshipCommandRequest : IRequest<CreateInternshipCommandResponse>
    {
        public Guid AdvisorID { get; set; }

        public Guid StudentID { get; set; }

        public string StudentNo { get; set; }

        public IFormFile formFile { get; set; }

        public string FullName { get; set; }

        public string TC_NO { get; set; }

        public string InternshipStartDate { get; set; }

        public string InternshipEndDate { get; set; }

        public string Department { get; set; }

        public string StudentGSMNumber { get; set; }

        public string CompanyName { get; set; }

        public int NumberOfEmployees { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyAddress { get; set; }

        public double RequestedGovernmentAidAmount { get; set; }

        public bool ReceivesSalary { get; set; }

        public bool DoesNotReceiveSalary { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public bool ReceivesHealthInsurance { get; set; }

        public DateOnly BirthDate { get; set; }//

        public string EmailSendingDate { get; set; }

        public InternshipLevel Level { get; set; }
    }

}
