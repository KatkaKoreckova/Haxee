namespace Haxee.Internal.Helpers;

public static class PasswordHelper
{
    const string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    const int PASSWORD_LENGTH = 8;

    /// <summary>
    /// Funkcia pre automatické gfenerovanie prístupového hesla pri požiadaní o resetovanie hesla ku kontu.
    /// </summary>
    public static string GeneratePassword()
    {
        var rand = new Random();
        var result = string.Empty;

        for(int i = 0;  i < PASSWORD_LENGTH; i++)
            result += ALPHABET[rand.Next(ALPHABET.Length)];

        return result;
    }
}
