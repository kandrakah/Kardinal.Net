namespace Kardinal.Net.Web.Auth
{
    /// <summary>
    /// Classe interna com os nomes de tabelas do Entity Framework.
    /// </summary>
    internal static class TableNames
    {

        internal static class System
        {
            internal const string SCHEMA = "stm";

            internal static class Security
            {
                internal const string PROTECTION_KEYS = "ProtectionKeys";
            }

            internal static class PermissionGroup
            {
                internal const string PERMISSIONS_GROUPS = "PermissionsGroups";

                internal const string PERMISSIONS = "Permissions";
            }
        }
    }
}
