namespace LibraryMVC.BL
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> UpdateAsync(T item);
        Task<bool> AddAsync(T item);
        Task<bool> DeleteByIdAsync(int id);
    }
}
