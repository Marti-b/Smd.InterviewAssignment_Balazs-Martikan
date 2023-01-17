namespace Smd.InterviewAssignment.WebApi.Services.Interfaces;

public interface IEmailService
{
    /// <summary>
    ///     Sends an email with a list of books available
    /// </summary>
    /// <param name="recipient"> The email address of the recipient </param>
    void SendEmail(string recipient);
}