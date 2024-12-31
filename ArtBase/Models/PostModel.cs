using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ArtBase.Models
{
    public class PostModel
    {
        [Key]
        public int PostID { get; set; } // Gönderi için benzersiz kimlik

        [Required]
        [MaxLength(280)] // Maksimum 280 karakter
        public string Content { get; set; } // Gönderi içeriği

        public DateTime CreatedAt { get; set; } // Gönderi tarihi

        // Identity User ile ilişki
        [Required]
        public string UserId { get; set; } // ASP.NET Identity User Id

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; } // Kullanıcı ile ilişki
    }
}
