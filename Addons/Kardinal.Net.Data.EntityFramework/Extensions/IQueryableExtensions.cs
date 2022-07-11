using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Coleção de objeto paginados.</returns>
        public static async Task<IPaginatedCollection<T>> ToPaginatedListAsync<T>([NotNull] this IQueryable<T> query, int skip, int take, CancellationToken cancellationToken = default)
        {
            var total = query.Count();
            var items = await query.Skip(skip).Take(take).ToListAsync(cancellationToken);
            return new PaginatedCollection<T>(items, total);
        }
    }
}
