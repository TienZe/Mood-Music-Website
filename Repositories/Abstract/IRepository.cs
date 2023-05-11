namespace PBL3.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T obj);
        void Update(T obj);
        void Delete(int id);
    }
}
