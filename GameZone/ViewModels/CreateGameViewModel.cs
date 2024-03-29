using GameZone.Attributes;
using GameZone.Settings;

namespace GameZone.ViewModels
{
    public class CreateGameViewModel : GameViewModel
    {
        [AllowedExtenstions(FileSettings.AllowedExtensions), AllowedMaxSize(FileSettings.MaxSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
