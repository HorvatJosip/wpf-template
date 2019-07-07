using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Core.Resources.FormValidation;

namespace Template.Core.DAL
{
    [Table("Login")]
    public class Login
    {
        [Required(ErrorMessageResourceName = nameof(LoginForm.UsernameError), ErrorMessageResourceType = typeof(LoginForm))]
        public string Username { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Password { get; set; }
    }
}
