namespace Haxee.Entities
{
    public static class Constants
    {
        public static class Roles
        {
            public const string User = "User";
            public const string Admin = "Admin";
        }

        public static class Policies
        {
            public const string RequireUser = "RequireUser";
            public const string RequireAdmin = "RequireAdmin";
        }

        public static class Emails
        {
            public const string SUPER_ADMIN = "superjozef@lstme.sk";
            public const string INSTRUCTOR = "jozef@lstme.sk";
            public const string KID = "ucastnik@lstme.sk";
        }

        public static class Paths
        {
            public const string LOG_IN = "/auth/login";
            public const string LOG_OUT = "/auth/logout";
        }

        public static class CascadingParameters
        {
            public const string CurrentAccount = "CurrentAccount";
        }
    }
}
