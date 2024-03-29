
using GameZone.Base;

namespace GameZone.Services
{
    public class DevicesServices : MainService<Device>,IDevicesServices
    {
        private readonly ApplicationDbContext _context;
        public DevicesServices(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetDevices()
        {
            return _context.Devices.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).OrderBy(d => d.Text).AsNoTracking().ToList();
        }
    }
}
