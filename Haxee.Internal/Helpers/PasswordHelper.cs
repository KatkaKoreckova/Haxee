namespace Haxee.Internal.Helpers;
public static class PasswordHelper
{
    const string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    const int PASSWORD_LENGTH = 8;

    public static string GeneratePassword()
    {
        var rand = new Random();
        var result = string.Empty;

        for(int i = 0;  i < PASSWORD_LENGTH; i++)
            result += ALPHABET[rand.Next(ALPHABET.Length)];

        return result;
    }
}
