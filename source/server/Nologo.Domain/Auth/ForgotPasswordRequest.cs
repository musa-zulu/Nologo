using System.ComponentModel.DataAnnotations;

namespace Nologo.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
