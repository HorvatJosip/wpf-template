using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Template.Core
{
    /// <summary>
    /// Used for working with services and setting up dependency injection.
    /// Use <see cref="Setup(Action{ServiceCollection})"/> to set it up.
    /// </summary>
    public static class Core
    {
        /// <summary>
        /// Provider for the collection of services for this project.
        /// </summary>
        public static IServiceProvider Provider { get; private set; }

        /// <summary>
        /// Configuration for this project.
        /// </summary>
        public static IConfigurationService Configuration => Get<IConfigurationService>();

        /// <summary>
        /// Logger used for this project.
        /// </summary>
        public static ILogger Logger => Get<ILogger>();

        /// <summary>
        /// Localizer used for this project.
        /// </summary>
        public static ILocalizerService Localizer => Get<ILocalizerService>();

        /// <summary>
        /// Used for setting up services.
        /// </summary>
        /// <param name="addServices">Optional method to add services to the collection.</param>
        public static void Setup(Action<ServiceCollection> addServices = null)
        {
            // Create service collection
            var services = new ServiceCollection();

            // Allow adding logging implementations
            services.AddLogging(loggingBuilder =>
            {
                // Add debug logger
                loggingBuilder.AddDebug();
            });

            // Add custom services
            services.AddSingleton<ISecurityService<DAL.Login>>(new SecurityService());

            // Add view models that require dependency injection
            services.AddSingleton(typeof(MainMenuPageViewModel));
            services.AddSingleton(typeof(LoginPageViewModel));

            // Allow the insertion of services
            addServices?.Invoke(services);

            // Build the provider
            Provider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Gets a service of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of service to get.</typeparam>
        public static T Get<T>() => Provider.GetService<T>();

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void Log(string message)
        {
            // If a logger exists...
            if (Logger != null)
                // Log the message with it
                Logger.Log(LogLevel.Information, message);

            // Otherwise...
            else
                // Log the message to Debug
                Debug.WriteLine($"Info: {message}");
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="exception">Exception to log.</param>
        public static void LogError(string message, Exception exception = null)
        {
            // If a logger exists...
            if (Logger != null)
                // Log the error with it
                Logger.Log(LogLevel.Error, exception, message);

            // Otherwise...
            else
            {
                // Log the error to Debug
                Debug.WriteLine($"Error: {message}");

                // If an exception was passed in...
                if (exception != null)
                    // Log it to Debug
                    Debug.WriteLine(exception);
            }
        }
    }
}
