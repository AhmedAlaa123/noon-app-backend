namespace noone.Reposatories
{
    public interface IReposatory<T>
    {
        bool Insert(T item);
        bool Delete(Guid Id);
        bool Update(Guid Id,T newItem);
        T GetById(Guid Id);
        ICollection<T> GetAll();
    }
}
