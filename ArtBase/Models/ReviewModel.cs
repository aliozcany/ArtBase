using System.ComponentModel.DataAnnotations;

namespace ArtBase.Models
{
    public class ReviewModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieID { get; set; } //bunlardan ikisinden biri ayrı tabloda tutulmalı
        public int SeriesID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public UserModel User { get; set; }
    }
}
