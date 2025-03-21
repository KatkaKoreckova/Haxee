using System.Text.Json;

namespace Haxee.Entities
{
    /// <summary>
    /// Konštanty často používaných hodnôt.
    /// </summary>
    public static class Constants
    {
        public const string QA_SEPARATOR = "---";

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
            public const string KID2 = "ucastnik2@lstme.sk";
        }

        public static class Paths
        {
            public const string LOG_IN = "/auth/login";
            public const string LOG_OUT = "/auth/logout";
        }

        public static class CascadingParameters
        {
            public const string CurrentAccount = "CurrentAccount";
            public const string RunningActivity = "RunningActivity";
        }

        public static JsonSerializerOptions DEFAULT_OPTIONS = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public static class Mqtt
        {
            public const string API_URL = "https://localhost:5000/";
        }

        public static class Limits
        {
            public const int MAX_PEOPLE = 100;
        }
    }
}
