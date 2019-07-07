using System;
using System.Security;
using System.Windows.Input;
using Template.Core.DAL;
using Template.Core.Resources.FormValidation;

namespace Template.Core
{
    public class LoginPageViewModel : BaseViewModel
    {
        public Login Login { get; set; }
        public ICommand LoginCommand { get; set; }
        public string Error { get; set; }

        public LoginPageViewModel() { }

        public LoginPageViewModel(IWindowService windowService, ISecurityService<Login> securityService)
        {
            LoginCommand = new RelayCommand<Func<SecureString>>(pwGetter =>
            {
                Error = "";

                var password = pwGetter();

                if (Validate(Login, (password.Length > 0, LoginForm.PasswordError, "Password")))
                {
                    if (securityService.Login(Login.Username, password))
                    {
                        windowService.ChangePage(Page.MainMenu);
                    }
                    else
                    {
                        Error = LoginForm.CredentialsError;
                    }
                }
            });
        }
    }
}
