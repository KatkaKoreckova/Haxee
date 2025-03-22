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
