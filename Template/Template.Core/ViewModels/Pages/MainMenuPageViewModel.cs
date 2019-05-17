using System.Windows.Input;

namespace Template.Core
{
    public class MainMenuPageViewModel : BaseViewModel
    {
        public ICommand SayHelloCommand { get; set; }
        public string Hello { get; set; }

        public MainMenuPageViewModel()
        {
            SayHelloCommand = new RelayCommand(() => Hello = "Hello");
        }
    }
}
