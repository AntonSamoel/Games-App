using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Base
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
