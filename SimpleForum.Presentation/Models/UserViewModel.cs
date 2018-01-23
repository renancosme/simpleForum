using System.ComponentModel.DataAnnotations;

namespace SimpleForum.Presentation.Models
{
    public class UserViewModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Login { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
