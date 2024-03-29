using GameZone.Base;

namespace GameZone.Services
{
    public class CategoriesServices : MainService<Category>, ICategoriesServices
    {
        private readonly ApplicationDbContext _context;
        public CategoriesServices(ApplicationDbContext context) :base(context) 
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetCategories()
        {
            return _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(c => c.Text).AsNoTracking().ToList();
        }


    }
}
