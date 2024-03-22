using InternshipManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.ViewModels.InternshipViewModelss
{
    public class InternshipCreateVM
    {
        public Guid AdvisorID { get; set; }
        public string StudentNo { get; set; }   
        public InternshipStatus? Status { get; set; }
        public Guid? FormID { get; set; }
        public Guid? ExcelID { get; set; }

    }
}
