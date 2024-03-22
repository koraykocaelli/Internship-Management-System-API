namespace InternshipManagementSystem.Domain.Entities
{
    public class Student : BaseEntity, IAppUserCreatable
    {
        public Guid? AdvisorID { get; set; }
        public string StudentNo { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string TC_NO { get; set; }
        public string FacultyName { get; set; }
        public string DepartmentName { get; set; }
        public string ProgramName { get; set; }
        public float GPA { get; set; }
        public string StudentGSMNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public ICollection<Internship>? Internships { get; set; }

        public Guid GetGuidID()
        {
            return ID;
        }

        public string GetUniqueIdentifier()
        {
            return StudentNo;
        }
    }

}
