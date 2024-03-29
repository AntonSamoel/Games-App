
using GameZone.Base;
using GameZone.Models;
using GameZone.Settings;

namespace GameZone.Services
{
    public class GameServices : MainService<Game>, IGameServices
    {
        private readonly ApplicationDbContext _context;

        public GameServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _context = context;
        }

        public async override Task<ICollection<Game>> GetAll()
        {
            return await _context.Games.Include(g => g.Category).Include(g => g.GameDevices).ThenInclude(gd => gd.Device).AsNoTracking().ToListAsync();
        }

        public async override Task<Game>  GetById(int id)
        {
            var item = await _context.Games.Include(g => g.Category).Include(g => g.GameDevices).ThenInclude(gd => gd.Device).SingleOrDefaultAsync(g=>g.Id==id);
            return item;
        }
    }
}
