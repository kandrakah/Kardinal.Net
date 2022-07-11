using System.Linq;

namespace Kardinal.Net
{
    /// <summary>
    /// Extensões para <see cref="IQueryable{T}"/>.
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Extensão que transforma obtém uma coleção de itens paginados.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto da coleção.</typeparam>
        /// <param name="query">Objeto referenciado.</param>
        /// <param name="skip">Itens à serem ignorados.</param>
        /// <param name="take">Itens à serem obtidos.</param>
        /// <returns>Coleção de objeto paginados.</returns>
        public static IPaginatedCollection<T> ToPaginatedList<T>(this IQueryable<T> query, int skip, int take)
        {
            var total = query.Count();
            var items = query.Skip(skip).Take(take).ToList();
            return new PaginatedCollection<T>(items, total);
        }
    }
}
