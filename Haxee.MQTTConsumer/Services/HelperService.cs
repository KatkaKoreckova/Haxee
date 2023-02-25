namespace Haxee.MQTTConsumer.Services
{
    public class HelperService
    {
        public static bool ValidMenuOption(List<int> validOptions, int option)
        {
            foreach (int o in validOptions)
                if (o == option)
                    return true;

            return false;
        }

        public static bool ValidateYear(string year)
        {
            return int.TryParse(year, out int y);
        }

        public static bool ValidateIp(string a)
        {

            List<string> ip = a.Split('.').ToList();

            if (ip.Count != 4)
                return false;

            foreach (string i in ip)
            {
                int current;
                if (!int.TryParse(i, out current))
                    return false;

                if (current < 0 || current > 255)
                    return false;
            }

            return true;
        }
    }
}
