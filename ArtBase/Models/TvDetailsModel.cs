namespace ArtBase.Models
{
    public class TvDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> episode_run_time { get; set; } // Bölüm süreleri

        public int? number_of_episodes { get; set; } // Toplam bölüm sayısı
    }
}
