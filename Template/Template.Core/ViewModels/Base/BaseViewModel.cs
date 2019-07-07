using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Template.Core
{
    /// <summary>
    /// Base view model for other view models.
    /// </summary>
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel
    {
        protected ILocalizerService localizer = Core.Localizer;

        /// <summary>
        /// Event that is fired after a validation has been performed.
        /// </summary>
        public event EventHandler<ValidationPerformedEventArgs> ValidationPerformed;

        /// <summary>
        /// Validates all of the properties on the view model.
        /// Returns true if the validation results in no errors.
        /// </summary>
        /// <param name="additionalChecks">Checks to perform aside from ones on properties.</param>
        /// <returns></returns>
        protected bool Validate(params (bool check, string error, string member)[] additionalChecks)
            => Validate(this, additionalChecks);

        /// <summary>
        /// Validates all of the properties on the given <paramref name="target"/>.
        /// Returns true if the validation results in no errors.
        /// </summary>
        /// <param name="target">Target object to validate.</param>
        /// <param name="additionalChecks">Checks to perform aside from ones on properties.</param>
        /// <returns></returns>
        protected bool Validate(object target, params (bool check, string error, string member)[] additionalChecks)
        {
            // Define the result as valid by default
            var valid = true;
            // Create a collection to pass to the event
            var allValidations = new List<Validations>();

            // Go through the properties of the target object
            foreach (var property in target.GetType().GetProperties())
            {
                // Skip property if it can be found in additional checks
                if (additionalChecks.Any(check => check.member == property.Name))
                    continue;

                // Create a new validation list for the current property
                var validations = new Validations();

                // Check for errors on the property
                var errors = validations.CheckForErrors(Validator.TryValidateProperty(
                    value: property.GetValue(target),
                    validationContext: new ValidationContext(target) { MemberName = property.Name },
                    validationResults: validations.Results
                ));

                // If there are errors...
                if (valid && string.IsNullOrWhiteSpace(errors) == false)
                    // Change validity to false
                    valid = false;

                // Add the current property validations to the collection
                allValidations.Add(validations);
            }

            // Go through additional checks
            foreach (var (check, error, member) in additionalChecks)
            {
                // Make a validation for the current one
                var validations = new Validations();

                // Add the result
                validations.AddValidationResult(error, member, check);

                // If it's invalid...
                if (valid && check == false)
                    // Change validity to false
                    valid = false;

                // Add the current additional check to the collection of validations
                allValidations.Add(validations);
            }

            // Signal that the validation has been performed
            ValidationPerformed?.Invoke(this, new ValidationPerformedEventArgs
            {
                // Pass in the validations that have been performed
                Validations = allValidations
            });

            // Return the result of validations
            return valid;
        }
    }
}