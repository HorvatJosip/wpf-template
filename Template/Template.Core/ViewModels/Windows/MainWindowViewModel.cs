using System.Windows.Input;

namespace Template.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Commands

        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand DragCommand { get; }

        #endregion

        #region Properties

        public Page Page { get; set; } 

        #endregion

        public MainWindowViewModel()
        {
            // Get the window service
            var windowService = Core.Get<IWindowService>();

            // Listen to page change event
            windowService.PageChanged += (sender, e) => Page = e.Page;

            // Setup the window commands
            MinimizeCommand = new RelayCommand(() => windowService.Minimize());
            MaximizeCommand = new RelayCommand(() => windowService.MaximizeOrRestore());
            CloseCommand = new RelayCommand(() => windowService.Close());
            DragCommand = new RelayCommand(() => windowService.DragMove());
        }
    }
}
