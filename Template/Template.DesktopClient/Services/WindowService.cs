using Template.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Template.DesktopClient
{
    /// <summary>
    /// Imeplementation of <see cref="IWindowService"/>.
    /// </summary>
    public class WindowService : IWindowService
    {
        private System.Windows.WindowState windowState;

        private List<Page> pageNavigator = new List<Page>();

        private List<Window> windowNavigator = new List<Window>();

        private List<System.Windows.Window> windows = new List<System.Windows.Window>();

        /// <summary>
        /// Event used for singalling that the <see cref="Page"/> changed.
        /// </summary>
        public event EventHandler<Page> PageChanged;

        /// <summary>
        /// Event used for singalling that the <see cref="Window"/> changed.
        /// </summary>
        public event EventHandler<Window> WindowChanged;

        /// <summary>
        /// Page that is currently being displayed.
        /// </summary>
        public Page Page
        {
            // Get the last page from the navigator
            get => pageNavigator.Last();
            set
            {
                // Get the index of wanted page
                var index = pageNavigator.IndexOf(value);

                // If it doesn't exist...
                if (index == -1)
                    // Append it to the end
                    pageNavigator.Add(value);

                // Otherwise...
                else
                {
                    // Declare where the page removal should start
                    var removeStart = index + 1;

                    // If it matches the count...
                    if (removeStart == pageNavigator.Count)
                        // It is the same as last page, so just bail
                        return;

                    // Remove the pages until the wanted one
                    pageNavigator.RemoveRange(removeStart, pageNavigator.Count - removeStart);
                }

                // Signal that the page has changed
                PageChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Window that is currently active.
        /// </summary>
        public Window Window
        {
            // Get the last window from the navigator
            get => windowNavigator.Last();
            set
            {
                // Get the index of wanted window
                var index = windowNavigator.IndexOf(value);

                // If it doesn't exist...
                if (index == -1)
                {
                    // Append it to the end
                    windowNavigator.Add(value);

                    // If we can't...
                    if (!Try.To(() =>
                     {
                         // Get the window by type name
                         var windowType = Type.GetType($"{GetType().Namespace}.{value}");

                         // Create an instance of it and add it to the collection of windows
                         windows.Add((System.Windows.Window)Activator.CreateInstance(windowType));
                     }))
                        // Bail
                        return;
                }

                // Otherwise...
                else
                {
                    // Declare where the window removal should start
                    var removeStart = index + 1;

                    // If it matches the count...
                    if (removeStart == windowNavigator.Count)
                        // It is the same as last window, so just bail
                        return;

                    // Calculate the remove count
                    var removeCount = windowNavigator.Count - removeStart;

                    // Remove the windows from navigator and window collection
                    windowNavigator.RemoveRange(removeStart, removeCount);
                    windows.RemoveRange(removeStart, removeCount);
                }

                // Signal that the window has changed
                WindowChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Currently active window.
        /// </summary>
        public System.Windows.Window CurrentWindow => windows.Last();

        /// <summary>
        /// Implementation of <see cref="IWindowService.Close"/>.
        /// </summary>
        public void Close()
        {
            CurrentWindow.Close();
        }

        /// <summary>
        /// Implementation of <see cref="IWindowService.DragMove"/>.
        /// </summary>
        public void DragMove()
        {
            Try.To(CurrentWindow.DragMove);
        }

        /// <summary>
        /// Implementation of <see cref="IWindowService.MaximizeOrRestore"/>.
        /// </summary>
        public void MaximizeOrRestore()
        {
            // Get the current window
            var window = CurrentWindow;

            // If its state is normal...
            windowState = window.WindowState == System.Windows.WindowState.Normal
                // It should be set to maximized
                ? System.Windows.WindowState.Maximized
                // Otherwise, it should be set to normal
                : System.Windows.WindowState.Normal;

            // Set the window state of the current window
            window.WindowState = windowState;
        }

        /// <summary>
        /// Implementation of <see cref="IWindowService.Minimize"/>.
        /// </summary>
        public void Minimize()
            => CurrentWindow.WindowState = System.Windows.WindowState.Minimized;

        /// <summary>
        /// Implementation of <see cref="IWindowService.SetWindowSize(double, double)"/>.
        /// </summary>
        public void SetWindowSize(double width, double height)
        {
            // Set the width and height of the current window
            CurrentWindow.Width = width;
            CurrentWindow.Height = height;
        }
    }
}
