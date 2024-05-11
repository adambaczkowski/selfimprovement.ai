using System.Net;
using System.Net.Mail;
using CData.EntityFrameworkCore.Gmail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using IdentityApi.Exceptions;
using IdentityApi.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace IdentityApi.Services;
public class EmailSender : IEmailSender
{
    private readonly GmailOptions _gmailOptions;
    public EmailSender(IOptions<GmailOptions> options)
    {
        _gmailOptions = options.Value;
    }
    public async Task SendEmailAsync(string to,string subject, string body)
    {
        var sender = new SmtpSender(() => new SmtpClient("host.docker.internal")
        {
            Port = 1025,
        });
        
        Email.DefaultSender = sender;
        
        var result = await Email
            .From("matiapptest@gmail.com")
            .To(to)
            .Subject(subject)
            .Body(body)
            .SendAsync();
        
        if (!result.Successful)
        {
            throw new ApiException("Something went wrong while sending email", "500");
        }
    }
}
