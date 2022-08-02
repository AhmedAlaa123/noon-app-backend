namespace noone.Reposatories
{
    public interface IReposatory<T>
    {
        bool Insert(T item);
        bool Delete(T item);
        bool Update(Guid Id);
        T GetById(Guid Id);
        ICollection<T> GetAll();
    }
}
