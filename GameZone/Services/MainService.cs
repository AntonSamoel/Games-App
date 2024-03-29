using GameZone.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class MainService<T> : IMainService<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public MainService(ApplicationDbContext _context)
        {
            context = _context;

        }

        public virtual async Task<bool> Create(T item)
        {
           await context.Set<T>().AddAsync(item);
            return await Save();
        }

        public async Task<bool> Delete(T item)
        {
            context.Set<T>().Remove(item);
            return await Save();

        }

        public async Task<bool> Exist(int id)
        {
            var item = await context.Set<T>().FindAsync(id);
            if (item is null)
                return false;
            return true;
        }

        public virtual async Task<ICollection<T>> GetAll()
        {
            var items = await context.Set<T>().ToListAsync();
            return items;
        }

        public virtual async Task<T> GetById(int id)
        {
            var item = await context.Set<T>().FindAsync(id);
            return item;
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync()>0;
        }

        public async Task<bool> Update(T item)
        {
            context.Set<T>().Update(item);
            return await Save();
        }
        
    }
}
