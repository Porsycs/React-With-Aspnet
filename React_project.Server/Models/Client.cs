using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace React_project.Server.Models
{
    public class Client
    {
        public Guid Id { get; set; } = new Guid();
        [Required]
        [DataType(DataType.Date)]
        public DateTime IncludeDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime? AlterationDate { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        [StringLength(100)]
        public string? Email { get; set; }
        [Required]
        [MaxLength(30)]
        public string? phone { get; set; }

        [Required]
        [MaxLength(30)]
        public string? document { get; set; }
        [Required]
        public bool active { get; set; } = true;

        [NotMapped]
        public string? operation { get; set; }
    }
}
