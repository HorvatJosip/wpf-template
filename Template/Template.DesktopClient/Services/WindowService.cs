using Template.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Template.DesktopClient
{
    /// <summary>
    /// Contains information about the window.
    /// </summary>
    public class WindowInfo
    {
        private Action<object, Page> pageChanged;
        private List<Page> pageNavigator = new List<Page>();

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
                pageChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Window id.
        /// </summary>
        public Window Window { get; set; }

        /// <summary>
        /// Actual window that can be displayed.
        /// </summary>
        public System.Windows.Window UI { get; set; }

        /// <summary>
        /// State of the window.
        /// </summary>
        public System.Windows.WindowState State { get; set; }

        /// <summary>
        /// Is this window active or not right now?
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Creates an instance of <see cref="WindowInfo"/> 
        /// with page changed event handler.
        /// </summary>
        /// <param name="pageChanged"></param>
        public WindowInfo(Action<object, Page> pageChanged)
        {
            this.pageChanged = pageChanged;
        }

        /// <summary>
        /// Maximizes or restores the window.
        /// </summary>
        public void MaximizeOrRestore()
        {
            // If the window's state is normal...
            State = UI.WindowState == System.Windows.WindowState.Normal
                // It should be set to maximized
                ? System.Windows.WindowState.Maximized
                // Otherwise, it should be set to normal
                : System.Windows.WindowState.Normal;

            // Set the window state of the current window
            UI.WindowState = State;
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        public void Close() => UI.Close();

        /// <summary>
        /// Tries to drag-move the window.
        /// </summary>
        public void DragMove() => Try.To(UI.DragMove);

        /// <summary>
        /// Minimizes the window
        /// </summary>
        public void Minimize() => UI.WindowState = System.Windows.WindowState.Minimized;

        /// <summary>
        /// Sets the width and height of the window.
        /// </summary>
        public void SetSize(double width, double height)
        {
            // Set the width and height of the current window
            UI.Width = width;
            UI.Height = height;
        }

        public override bool Equals(object obj) => obj is WindowInfo info && info.Window == Window;
        public override int GetHashCode() => Window.GetHashCode();
    }

    /// <summary>
    /// Imeplementation of <see cref="IWindowService"/>.
    /// </summary>
    public class WindowService : IWindowService
    {
        private List<WindowInfo> windowNavigator = new List<WindowInfo>();

        /// <summary>
        /// Event used for singalling that the <see cref="Page"/> changed.
        /// </summary>
        public event EventHandler<PageChangedEventArgs> PageChanged;

        /// <summary>
        /// Event used for singalling that the <see cref="Window"/> changed.
        /// </summary>
        public event EventHandler<Window> WindowChanged;

        /// <summary>
        /// Currently active window or null if there aren't any.
        /// </summary>
        public WindowInfo CurrentWindow => windowNavigator.Count > 0
            ? windowNavigator.First(info => info.Active)
            : null;

        /// <summary>
        /// Implementation of <see cref="IWindowService.Open(Window, WindowAction)"/>.
        /// </summary>
        public bool Open(Window window, WindowAction? previousWindowAction)
        {
            // Grab the previous window
            var previousWindow = CurrentWindow;

            // Define the result of the action and default it to true
            var success = true;

            // Get the index of wanted window
            var index = windowNavigator.IndexOf(info => info.Window == window);

            // If it doesn't exist...
            if (index == -1)
            {
                // Allow nulls for previous window action only if it is the first window
                if (windowNavigator.Count > 0 && previousWindowAction == null)
                    throw new InvalidOperationException("You have to specify what to do with previous window!");

                // Try to add it
                success = Try.To(() =>
                {
                    // Get the window by type name
                    var windowType = Type.GetType($"{typeof(Windows.MainWindow).Namespace}.{window}");

                    // Create instance of that window
                    var instance = (System.Windows.Window)Activator.CreateInstance(windowType);

                    // Create info about the window
                    var info = new WindowInfo((sender, page) => PageChanged?.Invoke(sender, new PageChangedEventArgs
                    {
                        Page = page,
                        Window = window
                    }))
                    {
                        UI = instance,
                        Window = window,
                        State = instance.WindowState,
                        Active = true // windowNavigator.Count == 0
                    };

                    // Whenever the window is activated, set it as the active window
                    instance.Activated += (sender, e) => windowNavigator.ForEach(i => i.Active = i.Window == window);

                    // Current window is active, make sure other's don't have the Active flag set
                    windowNavigator.ForEach(i => i.Active = false);

                    // Add info to the window navigator
                    windowNavigator.Add(info);
                });
            }

            // Otherwise...
            else
            {
                // Declare where the window removal should start
                var removeStart = index + 1;

                // If it matches the count...
                if (removeStart == windowNavigator.Count)
                    // It is the same as last window, so just bail
                    success = false;

                // Otherwise...
                else
                {
                    // Clear the windows starting from the end of the list
                    for (int i = windowNavigator.Count - 1; i >= removeStart; i--)
                        // Closing will in turn remove them from the navigator
                        windowNavigator[i].UI.Close();

                    // Make the window active
                    windowNavigator.Last().Active = true;
                }
            }

            if (success)
            {
                // Get currently active window
                var currentWindow = CurrentWindow;

                // Remove from navigator on close
                currentWindow.UI.Closing += (sender, e) => windowNavigator.Remove(currentWindow);

                // If there is a previous window...
                if (previousWindow != null)
                    switch (previousWindowAction)
                    {
                        case WindowAction.Hide:
                            previousWindow.UI.Hide();
                            currentWindow.UI.ShowDialog();
                            previousWindow.UI.Show();
                            break;
                        case WindowAction.Close:
                            previousWindow.UI.Hide();
                            currentWindow.UI.ShowDialog();
                            Close(previousWindow.Window);
                            break;
                        case WindowAction.LeaveShown:
                            currentWindow.UI.ShowDialog();
                            break;
                        case WindowAction.LeaveShownAndInteractable:
                            currentWindow.UI.Show();
                            break;
                    }

                // Signal that the window has changed
                WindowChanged?.Invoke(this, window);
            }

            return success;
        }

        private bool DoStuff(Action<WindowInfo> action, Window? window = null)
        {
            var info = window == null
                ? CurrentWindow
                : windowNavigator.FirstOrDefault(i => i.Window == window);

            if (info == null)
                return false;

            action(info);
            return true;
        }

        private bool ExecuteAction(Window? window, object[] parameters = null, [CallerMemberName]string action = null)
            => DoStuff(info => typeof(WindowInfo).GetMethod(action).Invoke(info, parameters), window);

        /// <summary>
        /// Implementation of <see cref="IWindowService.ChangePage(Page, Window?)"/>.
        /// </summary>
        public bool ChangePage(Page page, Window? window = null)
            => DoStuff(info => info.Page = page, window);

        /// <summary>
        /// Implementation of <see cref="IWindowService.Close(Window?)"/>.
        /// </summary>
        public bool Close(Window? window = null) => ExecuteAction(window);

        /// <summary>
        /// Implementation of <see cref="IWindowService.DragMove(Window?)"/>.
        /// </summary>
        public bool DragMove(Window? window = null) => ExecuteAction(window);

        /// <summary>
        /// Implementation of <see cref="IWindowService.Minimize(Window?)"/>.
        /// </summary>
        public bool Minimize(Window? window = null) => ExecuteAction(window);

        /// <summary>
        /// Implementation of <see cref="IWindowService.MaximizeOrRestore(Window?)"/>.
        /// </summary>
        public bool MaximizeOrRestore(Window? window = null) => ExecuteAction(window);

        /// <summary>
        /// Implementation of <see cref="IWindowService.ChangeSize(double, double, Window?)"/>.
        /// </summary>
        public bool ChangeSize(double width, double height, Window? window = null)
            => ExecuteAction(window, new object[] { width, height });
    }
}
