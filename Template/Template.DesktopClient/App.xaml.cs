using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Template.Core;

namespace Template.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Create the window service
            var windowService = new WindowService();

            // Create the localizer service
            var localizerService = new LocalizerService();

            // Setup the dependencies
            Core.Core.Setup(collection =>
            {
                // Add services to the service collection
                collection.AddSingleton<IWindowService>(windowService);
                collection.AddSingleton<IChooserDialogsService>(new ChooserDialogsService());
                collection.AddSingleton<IConfigurationService>(new ConfigurationService());
                collection.AddSingleton<ILocalizerService>(localizerService);
            });

            // Setup the default window style in order to set application font
            var type = typeof(System.Windows.Window);
            FrameworkElement.StyleProperty.OverrideMetadata(type, new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(type)
            });

            // Setup current language based on starting languages in resource dictionaries
            localizerService.ChangeLanguage("en-US");

            // Setup the starting window and page using the window service
            windowService.Open(Core.Window.MainWindow, null);
            Current.MainWindow = windowService.CurrentWindow.UI;
            windowService.ChangePage(Page.Login);
            Current.MainWindow.ShowDialog();
        }
    }
}
