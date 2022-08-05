namespace noone.Reposatories
{
    public interface IReposatory<T>
    {

        Task<bool> Insert(T item);
        Task<bool> Delete(Guid Id);
        Task<bool> Update(Guid Id,T Item);
        Task<T> GetById(Guid Id);
        Task<ICollection<T>> GetAll();

    }
}
