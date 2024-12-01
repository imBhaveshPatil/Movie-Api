namespace MovieWebAppApi.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }

        public DateTime ReleasedDate { get; set; }

    }
}
