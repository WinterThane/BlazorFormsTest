using BlazorFormsTest.Models;

namespace BlazorFormsTest.Service
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Contact contact);
    }
}
