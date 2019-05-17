using System.Windows.Controls;

namespace Template.DesktopClient
{
    /// <summary>
    /// Page that sets up the view model as DataContext.
    /// </summary>
    /// <typeparam name="VM">View model to use for DataContext.</typeparam>
    public class BasePage<VM> : Page where VM : Core.BaseViewModel, new()
    {
        public BasePage()
        {
            // Setup the data context using the view model
            DataContext = Core.Core.Get<VM>() ?? new VM();
        }
    }
}
