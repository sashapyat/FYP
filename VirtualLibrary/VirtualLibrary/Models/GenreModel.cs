namespace VirtualLibrary.Models
{
    public class GenreModel
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public bool IsSelected { get; set; }
        public List<int> GenresSelected { get; set; }
    }
}
