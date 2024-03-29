namespace GameZone.Interfaces
{
    public interface IMainService<T> where T : class
    {
        public Task<ICollection<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<bool> Create(T item);
        public Task<bool> Update(T item);
        public Task<bool> Delete(T item);
        public Task<bool> Exist(int id);
        public Task<bool> Save();
        
    }
}
