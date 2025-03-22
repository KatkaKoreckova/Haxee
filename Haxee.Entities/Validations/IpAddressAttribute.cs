namespace Haxee.Entities.Validations
{
    public class IpAddressAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.MemberName is null)
                throw new ArgumentException();

            if (value is null)
                return ValidationResult.Success;

            if (value is not string ip || !ValidateIp(ip))
                return new ValidationResult("Invalid IP address format.", new List<string>() { validationContext.MemberName });

            return ValidationResult.Success;
        }

        private static bool ValidateIp(string ipAddress)
        {
            var ipParts = ipAddress.Split('.').ToList();

            if (ipParts.Count != 4)
                return false;

            foreach (string part in ipParts)
            {
                if (!int.TryParse(part, out int current) || current < 0 || current > 255)
                    return false;
            }

            return true;
        }
    }
}
