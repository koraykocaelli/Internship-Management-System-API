using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.ViewModels.StudentViewModels
{
   public class VM_Add_Student_to_Advisor
    {
        public Guid AdvisorID { get; set; }
        
        public Guid StudentID { get; set; }
    }
}
