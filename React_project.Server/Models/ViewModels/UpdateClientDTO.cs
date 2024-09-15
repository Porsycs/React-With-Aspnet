using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace React_project.Server.Models.ViewModels
{
    public class UpdateClientDTO
    {
        [Required]
        public Guid Id { get; set; } = Guid.Empty;
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
        public bool active { get; set; }
    }
}
