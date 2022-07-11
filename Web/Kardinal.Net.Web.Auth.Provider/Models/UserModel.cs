using System;

namespace Kardinal.Net.Web.Auth.Provider
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }
    }
}
