namespace InternshipManagementSystem.Domain.Entities
{
    public class InternshipDocument : BaseEntity
    {
        public Guid InternshipID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class SPAS : InternshipDocument
    {
    }

    public class InternshipApplicationForm : InternshipDocument
    {
    }
    public class InternshipBook : InternshipDocument
    {

    }
}

