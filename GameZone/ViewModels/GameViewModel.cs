using GameZone.Attributes;
using GameZone.Models;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels
{
    public class GameViewModel
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        [Display(Name = "Suported Devices")]
        public List<int> SelectedDevices { get; set; } 
        public IEnumerable<SelectListItem> Devices { get; set; } = new List<SelectListItem>();
        public ICollection<GameDevice> GameDevices { get; set; } = new List<GameDevice>();
        
    }
}
