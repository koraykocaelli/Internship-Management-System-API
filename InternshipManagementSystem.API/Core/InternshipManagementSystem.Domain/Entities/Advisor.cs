namespace InternshipManagementSystem.Domain.Entities
{
    public class Advisor : BaseEntity, IAppUserCreatable
    {
        public string FacultyName { get; set; }
        public string AdvisorName { get; set; }
        public string AdvisorSurname { get; set; }
        public string TC_NO { get; set; }
        public string DepartmentName { get; set; }
        public string ProgramName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public ICollection<Student>? Students { get; set; }
        public ICollection<Internship>? Internships { get; set; }

        public Guid GetGuidID()
        {
            return ID;
        }

        public string GetUniqueIdentifier()
        {
            return Email;
        }
    }

    public interface IAppUserCreatable
    {
        string GetUniqueIdentifier();
        Guid GetGuidID();
    }
}

