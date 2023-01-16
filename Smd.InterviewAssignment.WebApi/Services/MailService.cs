using System.Linq;
using System.Text.Json;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Smd.InterviewAssignment.WebApi.Data;
using Smd.InterviewAssignment.WebApi.Services.Interfaces;

namespace Smd.InterviewAssignment.WebApi.Services;

public class MailService : IMailService
{
    private readonly BookContext _bookContext;
    
    public MailService(BookContext bookContext)
    {
        _bookContext = bookContext;
    }
    
    public void SendEmail(string recipient)
    {
        var listOfBookObjects = _bookContext.Books.ToList();
        string listOfBooks = JsonSerializer.Serialize(listOfBookObjects);
            
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("noreply@dba.dk"));
        email.To.Add(MailboxAddress.Parse(recipient));
        email.Subject = ("New books today");
        email.Body = new TextPart(TextFormat.Text)
        {
            Text = $"Here is a list of new books: {listOfBooks}"
        };
            
        using var smtp = new SmtpClient();
        smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("oleta.davis@ethereal.email", "Bacq26PEvtnjVxxVwJ");
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}