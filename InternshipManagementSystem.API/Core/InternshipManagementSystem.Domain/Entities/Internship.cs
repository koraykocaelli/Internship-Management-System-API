namespace InternshipManagementSystem.Domain.Entities
{

    public class Internship : BaseEntity
    {
        public Guid AdvisorID { get; set; }
        public Guid StudentID { get; set; }
        public string StudentNo { get; set; }
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public List<string>? StudentNotifications { get; set; }
        public List<string>? AdvisorNotifications { get; set; }
        public InternshipStatus InternshipStatus { get; set; } = 0;
        public Guid? InternshipApplicationFormID { get; set; }
        public Guid? InternshipApplicationExelFormID { get; set; }
        public Guid? InternshipBookID { get; set; }
        public Guid? SPASID { get; set; }
    }
    public enum InternshipStatus
    {
        Deleted = -1, // Silindi
        Pending = 0, // Beklemede
        ApplicationRejected = 1, // Staj başvurusu reddedildi
        InternshipStartApproved = 2, // Staj başlatma onaylandı,
        ModificationRequiredInStartInformation = 3, // Staj başlatma bilgilerinde düzenleme gerekli
        FilesAwaited = 4, // Staj dosyaları bekleniyor
        FilesUnderReview = 5, // Staj dosyaları incelemede
        FilesApproved = 6, // Staj dosyaları onaylandı
        DocumentsAwaitingSubmission = 7, // Staj evrakları teslim bekleniyor
        DocumentsSubmitted = 8, // Staj evrakları teslim edildi
        DocumentsNotSubmitted = 9, // Staj evrakları teslim edilmedi
        DocumentsUnderReview = 10, // Staj evrakları kontrol ediliyor
        InternshipCompleted = 11, // Staj tamamlandı
        InternshipCanceled = 12 // Staj iptal edildi
    }


}