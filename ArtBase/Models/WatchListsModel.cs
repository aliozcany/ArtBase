using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ArtBase.Models
{
    public class WatchListsModel
    {
        
            [Key]
            public int WatchlistID { get; set; } // Birincil anahtar

            public string Title { get; set; } // Dizi/Film adı

            public string PosterPath { get; set; } // Poster URL'si

            public string ContentType { get; set; } // "movie" veya "tv" gibi tür bilgisi

            public string Overview { get; set; } // Dizi/Film açıklaması
            public int? Runtime { get; set; } // Toplam süre (dakika cinsinden)
            public string UserId { get; set; } // Kullanıcı ID'si

            [ForeignKey("UserId")]
            public virtual IdentityUser User { get; set; } // Kullanıcı ile ilişki


        

    }
}

