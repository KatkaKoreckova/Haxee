using Microsoft.AspNetCore.Components;
using NETCore.MailKit.Core;

namespace Haxee.Internal.Services;

/// <summary>
/// Služba na odosielanie mailov
/// </summary>
public class MailService
{
    /// <summary>
    /// E-mailová služba
    /// </summary>
    private readonly IEmailService _emailService;

    /// <summary>
    /// Cesta k predlohám na mail.
    /// </summary>
    private static readonly string BASE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "emails");

    /// <summary>
    /// Cesta ku konkrétnemu mailu
    /// </summary>
    private static readonly string NEW_PASSWORD = Path.Combine(BASE_PATH, "NewPassword.html");

    /// <summary>
    /// Konštruktor triedy
    /// </summary>
    /// <param name="emailService">E-mailová služba</param>
    public MailService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    /// <summary>
    /// Funkcia na odoslanie nového hesla
    /// </summary>
    public async Task SendNewPasswordAsync(string emailAddress, string password)
        => await SendEmailAsync(emailAddress, "New password", NEW_PASSWORD, new Dictionary<string, string>
        {
            { "{PASSWORD}", password }
        });

    /// <summary>
    /// Funkcia na odoslanie mailu
    /// </summary>
    private async Task SendEmailAsync(string emailAddress, string subject, string bodyPath, Dictionary<string, string>? data = null)
    {
        if (!File.Exists(bodyPath))
            throw new FileNotFoundException("Could not find e-mail body file when sending e-mail.");

        string body = File.ReadAllText(bodyPath);

        if (data is not null)
            foreach (var dataPoint in data)
                body = body.Replace(dataPoint.Key, dataPoint.Value);

        await _emailService.SendAsync(emailAddress, $"{subject} — LSTME", body, true);
    }
}