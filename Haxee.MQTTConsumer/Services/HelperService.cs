using Haxee.Entities.Entities.Mqtt;
using Haxee.Entities.Enums;
using System.Linq;

namespace Haxee.MQTTConsumer.Services
{
    /// <summary>
    /// Trieda obsahujúca pomocné funkcie.
    /// </summary>
    public class HelperService
    {
        /// <summary>
        /// Kontrola, či zadaná možnosť je zo zoznamu možností.
        /// </summary>
        /// <param name="validOptions">Zoznam valídnych možností</param>
        /// <param name="option">zvolená možnosť</param>
        /// <returns>Či je zvolená možnosť valídna</returns>
        public static bool ValidMenuOption(List<int> validOptions, int option)
        {
            foreach (int o in validOptions)
                if (o == option)
                    return true;

            return false;
        }

        /// <summary>
        /// Kontrola, či zadaný rok je valídny
        /// </summary>
        /// <param name="year">Zadaný rok</param>
        /// <returns>Či je zadaný rok valídny</returns>
        public static bool ValidateYear(string year)
        {
            return int.TryParse(year, out int y);
        }

        /// <summary>
        /// Kontrola, či zadaná IP adresa je valídna.
        /// </summary>
        /// <param name="a">Zadaná IP adresa</param>
        /// <returns>Či je zadaná IP adresa valídna</returns>
        public static bool ValidateIp(string a)
        {

            List<string> ip = a.Split('.').ToList();

            if (ip.Count != 4)
                return false;

            foreach (string i in ip)
            {
                if (!int.TryParse(i, out int current))
                    return false;

                if (current < 0 || current > 255)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Kontrola, či zadaný port je valídny.
        /// </summary>
        /// <param name="p">Zadaný port</param>
        /// <returns>Či je zadaný port valídny</returns>
        public static bool ValidatePort(string p)
        {

            if (!int.TryParse(p, out int port))
                return false;

            if (port > 49151)
                return false;

            return true;
        }

        /// <summary>
        /// Kontrola, či zadaná téma je valídna.
        /// </summary>
        /// <param name="topic">Zadaná téma</param>
        /// <returns>Či je zadaná téma valídna</returns>
        public static bool ValidateTopic(string topic)
        {
            if (topic == String.Empty || topic.Length < 3)
                return false;

            if (topic.First().Equals('/') || topic.First().Equals('$'))
                return false;

           // if (!(topic.Substring(topic.Length - 2, 2)).Equals("/#"))
           //     return false;

            List<string> forbiddenChars = new List<string>(){ "#", "+", ">", "*", "$", "//", "\\" };

            foreach (string c in forbiddenChars)
            {
                if (c == "#" && topic.Contains(c))
                {
                    if (topic.Count(x => x == '#') > 1)
                        return false;

                    if (topic.Last() != '#')
                        return false;
                }
                else if (topic.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Kontrola, či zadaná konfigurácia ročníka je valídna.
        /// </summary>
        /// <param name="year">Zadaný rok</param>
        /// <param name="topic">Zadaná téma</param>
        /// <param name="port">Zadaný port</param>
        /// <param name="ip">Zadaná IP adresa</param>
        /// <returns>Či je zadaná konfigurácia valídna</returns>
        public static bool ValidateCurrentYearSetup(string year, string topic, string port, string ip)
        {
            bool valid = true;

            if (!ValidateYear(year))
            {
                valid = false;
                DrawService.DrawErrorMessage("YEAR not valid");
            }

            if (!ValidateTopic(topic))
            {
                valid = false;
                DrawService.DrawErrorMessage("TOPIC not valid");
            }

            if (!ValidatePort(port))
            {
                valid = false;
                DrawService.DrawErrorMessage("PORT not valid");
            }

            if (!ValidateIp(ip))
            {
                valid = false;
                DrawService.DrawErrorMessage("IP not valid");
            }
            return valid;
        }

        /// <summary>
        /// Získanie statusu zo vstupného reťazca.
        /// </summary>
        /// <param name="s">text obsahujúci status</param>
        /// <returns>Enum statusu ročníka</returns>
        public static YearStatus? GetStatusFromString(string s)
        {
            switch(s) {
                case "Waiting":
                    return YearStatus.Pending;
                case "Working":
                    return YearStatus.InProgress;
                case "Done":
                    return YearStatus.Finished;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Rozparsuje správu z MQTT brokera do objektu AttendeeInformation
        /// </summary>
        /// <param name="message">prijatá práva</param>
        /// <param name="topic">téma pod ktorou prišla</param>
        /// <returns>Objekt AttendeeInformationDTO alebo null ak sa nepodarilo vytvoriť objekt</returns>
        public static AttendeeInformationDTO? ParseMessage(string message, string topic)
        {
            List<string> messageParts = message.Split('|').ToList();

            if (messageParts.Count != 2)
                return null;

            DateTime dateTime;
            if (!DateTime.TryParse(messageParts[1], out dateTime))
                return null;

            //YearStatus? s = GetStatusFromString(m[2]);

            //if (s is null)
              //  return null;

            AttendeeInformationDTO attendeeInformation = new AttendeeInformationDTO
            {
                CardId = messageParts[0].Trim().Replace(" ", ":"),
                DateTime = dateTime,
                Trigger = topic[(topic.IndexOf('/')+1)..]
            };

            return attendeeInformation;
        }
    }
}
