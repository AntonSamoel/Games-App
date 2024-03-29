
namespace GameZone.Base
{
    public interface ICategoriesServices : IMainService<Category>
    {
        public IEnumerable<SelectListItem> GetCategories();
    }
}
