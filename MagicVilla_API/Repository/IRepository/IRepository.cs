using MagicVilla_API.Models.Especificaciones;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        Task Crear(T entity);

        Task<List<T>> Obtenertodos(Expression<Func<T, bool>>? filtro = null, string? includeProperties = null);

        PagedList<T> ObtenertodosPaginado(Parametros parametros, Expression<Func<T, bool>>? filtro = null, string? includeProperties = null);

        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true, string? includeProperties = null);

        Task Remover(T entity);

        Task Grabar();
    }
}
