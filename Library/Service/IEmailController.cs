namespace WebApplication2.service;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}