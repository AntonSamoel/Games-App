namespace GameZone.Models
{
    public class Category : Base
    {
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
