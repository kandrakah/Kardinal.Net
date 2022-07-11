using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    public interface IPermissionsService<TUser> :IDisposable where TUser : class
    {
        IEnumerable<string> GetPermissionsAsync(TUser user, CancellationToken cancellationToken = default);

        Task AddPermissionAsync(TUser user, string permission, CancellationToken cancellationToken = default);

        Task AddPermissionsAsync(TUser user, IEnumerable<string> permissions, CancellationToken cancellationToken = default);

        Task RemovePermissionAsync(TUser user, string permission, CancellationToken cancellationToken = default);

        Task RemovePermissionsAsync(TUser user, IEnumerable<string> permissions, CancellationToken cancellationToken = default);
    }
}
