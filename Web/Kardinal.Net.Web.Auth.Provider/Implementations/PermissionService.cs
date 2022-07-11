using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Auth
{
    public class PermissionService<TUser> : IPermissionsService<TUser> where TUser : class
    {
        public IEnumerable<string> GetPermissionsAsync(TUser user, CancellationToken cancellationToken = default)
        {
            return Enumerable.Empty<string>();
        }

        public Task AddPermissionAsync(TUser user, string permission, CancellationToken cancellationToken = default)
        {
            var permissions = new List<string> { permission };
            return this.AddPermissionsAsync(user, permissions, cancellationToken);
        }

        public Task AddPermissionsAsync(TUser user, IEnumerable<string> permissions, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task RemovePermissionAsync(TUser user, string permission, CancellationToken cancellationToken = default)
        {
            var permissions = new List<string> { permission };
            return this.RemovePermissionsAsync(user, permissions, cancellationToken);
        }

        public Task RemovePermissionsAsync(TUser user, IEnumerable<string> permissions, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
