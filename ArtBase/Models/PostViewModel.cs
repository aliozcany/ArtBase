﻿namespace ArtBase.Models
{
    public class PostViewModel
    {
        public int PostID { get; set; }
        public string Content { get; set; } // Gönderi içeriği
        public DateTime CreatedAt { get; set; } // Gönderi tarihi
        public string Username { get; set; } // Kullanıcı adı
    }

}
