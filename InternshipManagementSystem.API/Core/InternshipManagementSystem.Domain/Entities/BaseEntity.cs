using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipManagementSystem.Domain.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Guid ID { get; set; }    

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
