using Microsoft.Extensions.DependencyInjection;
using Template.Core;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace Template.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Setup the localization and globalization
            ///*
            var culture = new CultureInfo("hr-HR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            //*/

            // Create the window service
            var windowService = new WindowService();

            // Setup the dependencies
            Core.Core.Setup(collection =>
            {
                // Add the window service to the service collection
                collection.AddSingleton<IWindowService>(windowService);

                // Add the chooser dialogs service to the service collection
                collection.AddSingleton<IChooserDialogsService>(new ChooserDialogsService());

                // Add the configuration service to the service collection
                collection.AddSingleton<IConfigurationService>(new ConfigurationService());
            });

            // Setup the default window style in order to set application font
            var type = typeof(System.Windows.Window);
            FrameworkElement.StyleProperty.OverrideMetadata(type, new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(type)
            });

            // Setup the starting window and page using the window service
            windowService.Window = Core.Window.MainWindow;
            Current.MainWindow = windowService.CurrentWindow;
            windowService.Page = Page.MainMenu;
            Current.MainWindow.ShowDialog();
        }
    }
}
