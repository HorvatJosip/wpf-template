using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Template.Core;

namespace Template.DesktopClient
{
    /// <summary>
    /// Interaction logic for Validator.xaml
    /// </summary>
    public partial class Validator : UserControl
    {
        /// <summary>
        /// Object to validate (Pass in property name). Can also be a binding to error message.
        /// </summary>
        public object Validate
        {
            get { return GetValue(ValidateProperty); }
            set { SetValue(ValidateProperty, value); }
        }

        public static readonly DependencyProperty ValidateProperty =
            DependencyProperty.Register("Validate", typeof(object), typeof(Validator), new PropertyMetadata(null, OnValidatePropertyChanged));

        /// <summary>
        /// Option for <see cref="StringToVisibilityConverter"/> parameter. Defaults to null which
        /// will result in default behavior (collapse when there is no error, show when there is).
        /// </summary>
        public string VisibilityOption
        {
            get { return (string)GetValue(VisibilityOptionProperty); }
            set { SetValue(VisibilityOptionProperty, value); }
        }

        public static readonly DependencyProperty VisibilityOptionProperty =
            DependencyProperty.Register("VisibilityOption", typeof(string), typeof(Validator), new PropertyMetadata(null));

        public Validator()
        {
            InitializeComponent();
        }

        private static void OnValidatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Instance of the current class
            var instance = d as Validator;

            // Label that will show the error
            var label = instance.lblError;

            // Updates label with the given content and sets its visibility based on it
            void UpdateLabel(object content)
            {
                label.Visibility = (Visibility)StringToVisibilityConverter.Instance.Convert(content, null, instance.VisibilityOption, null);
                label.Content = content;
            }

            // Get the binding from the dependency property
            var binding = BindingOperations.GetBinding(d, e.Property);

            // If there is just a binding to the error message...
            if (binding != null)
                // Use the value that is stored in the bound property
                UpdateLabel(e.NewValue);

            // Otherwise...
            else
            {
                // Context that defines what is bound to the page or window
                var context = instance.DataContext;

                // Type of the item that is bound
                var type = context?.GetType();

                // Get name of the property that needs to be validated
                var toValidate = instance.Validate?.ToString();

                // Get the property that needs to be validated (in case a string is passed into Validate dependency property)
                var property = type.GetNestedProperty(context, toValidate);

                // Extract name from property or, if it's null, use the Validate property value
                var name = property?.Name ?? toValidate;

                Utils.PerformOnce($"ValidationPerformed for {name}", () =>
                    // Listen for validations to update the label
                    type.GetEvent("ValidationPerformed").AddEventHandler(
                        context,
                        new EventHandler<ValidationPerformedEventArgs>((sender, args) =>
                            UpdateLabel(args.Validations.Find(x => x.Member == name)?.Errors)
                        )
                    )
                );
            }
        }
    }
}
