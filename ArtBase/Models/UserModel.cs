using System.ComponentModel.DataAnnotations;

namespace ArtBase.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public RoleModel Role { get; set; }
    }
}
