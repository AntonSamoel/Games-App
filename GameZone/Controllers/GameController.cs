using GameZone.Base;
using GameZone.Services;
using GameZone.Settings;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.IO;
namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        private readonly ICategoriesServices categoriesServices;
        private readonly IDevicesServices devicesServices;
        private readonly IGameServices gameServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;

        public GameController(ICategoriesServices categoriesServices, IDevicesServices devicesServices, IGameServices gameServices, IWebHostEnvironment webHostEnvironment)
        {
            this.categoriesServices = categoriesServices;
            this.devicesServices = devicesServices;
            this.gameServices = gameServices;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task<IActionResult> Index()
        {
            var games = await gameServices.GetAll();
            return View(games);
        }
        public async Task<IActionResult> Details(int id)
        {
            var game = await gameServices.GetById(id);

            if (game is null)
                return NotFound();  

            return View(game);
        }

        [HttpGet]   
        public IActionResult Create()
        {
            CreateGameViewModel createGameViewModel = new() 
            {
            Categories = categoriesServices.GetCategories(),
            Devices =devicesServices.GetDevices(),
            };
            return View(createGameViewModel);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameViewModel gameViewModel)
        {
            if(!ModelState.IsValid)
            {
                gameViewModel.Categories = categoriesServices.GetCategories();
                gameViewModel.Devices = devicesServices.GetDevices();
                return View(gameViewModel);
            }

            // Create the name of the file which wii be saved on the server
            var coverName = await SaveCover(gameViewModel.Cover);

            Game game = new()
            {
                Name = gameViewModel.Name,
                Description = gameViewModel.Description,
                Cover = coverName,
                CategoryId = gameViewModel.CategoryId,
                GameDevices = gameViewModel.SelectedDevices.Select(sd => new GameDevice { DeviceId = sd }).ToList(),
            };

            await  gameServices.Create(game);

            return RedirectToAction("Index");
         
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await gameServices.GetById(id);
            if (game is null)
                return NotFound();

            EditGameViewModel editGameViewModel = new EditGameViewModel()
                {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.GameDevices.Select(d => d.DeviceId).ToList(),
                Categories = categoriesServices.GetCategories(),
                Devices = devicesServices.GetDevices(),
                CurrentCover = game.Cover
                };
            return View(editGameViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGameViewModel editGameViewModel)
        {
            if (!ModelState.IsValid)
            {
                editGameViewModel.Categories = categoriesServices.GetCategories();
                editGameViewModel.Devices = devicesServices.GetDevices();
                return View(editGameViewModel);
            }

            var game = await gameServices.GetById(editGameViewModel.Id);

            if (game is null)
                return NotFound();

            var hasNewCover = editGameViewModel.Cover is not null;
            var oldCover = game.Cover;

            game.Name = editGameViewModel.Name;
            game.Description = editGameViewModel.Description;
            game.CategoryId = editGameViewModel.CategoryId;
            game.GameDevices = editGameViewModel.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
                game.Cover = await SaveCover(editGameViewModel.Cover!);

            if (await gameServices.Update(game))
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagePath, oldCover);
                    System.IO.File.Delete(cover);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                System.IO.File.Delete(cover);
                return BadRequest();    
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await gameServices.GetById(id);    

            if(game is null) 
                return NotFound();

            if (await gameServices.Delete(game))
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                System.IO.File.Delete(cover);
                return Ok();
            }
            else
                return BadRequest();
        }

        public async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagePath, coverName);

            using var stream = System.IO.File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
    }
}
