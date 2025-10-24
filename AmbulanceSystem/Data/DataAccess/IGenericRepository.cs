namespace AmbulanceSystem.Core.Data;

public interface IGenericRepository<T>
    where T : class
{
    public T GetById(int id);
    public void Add(T entity);
    public void Update(T entity);
    public void Remove(int id);
    public List<T> GetAll();
    public T? FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    public IQueryable<T> GetQueryable();
}
