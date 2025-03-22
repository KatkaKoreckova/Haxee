namespace Haxee.Entities.Validations
{
    public class MqttTopicAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.MemberName is null)
                throw new ArgumentException();

            if (value is null)
                return ValidationResult.Success;

            if (value is not string topic || !ValidateTopic(topic))
                return new ValidationResult("Invalid MQTT topic format.", new List<string>() { validationContext.MemberName });

            return ValidationResult.Success;
        }

        private static bool ValidateTopic(string topic)
        {
            if (string.IsNullOrEmpty(topic) || topic.Length < 3)
                return false;

            if (topic.First().Equals('/'))
                return false;

            var forbiddenChars = new List<string>() { "#", "+", ">", "*", "$", "//", "\\" };

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
    }
}
