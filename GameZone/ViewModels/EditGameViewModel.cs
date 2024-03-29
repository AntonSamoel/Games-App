using GameZone.Attributes;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameZone.ViewModels
{
    public class EditGameViewModel : GameViewModel
    {
        public int Id { get; set; }
        public string? CurrentCover { get; set; }

        [AllowedExtenstions(FileSettings.AllowedExtensions,ErrorMessage ="Not Valid Extension"), AllowedMaxSize(FileSettings.MaxSizeInBytes,ErrorMessage ="Not Valid Size")]
        [ValidateNever]
        public IFormFile? Cover { get; set; } = default!;
    }
}
