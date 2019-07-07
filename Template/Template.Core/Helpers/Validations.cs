using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Template.Core
{
    /// <summary>
    /// Collection of validation results.
    /// </summary>
    public class Validations
    {
        public List<ValidationResult> Results { get; set; } = new List<ValidationResult>();

        /// <summary>
        /// Member that the validations were performed on.
        /// </summary>
        public string Member => string.Join(", ", Results.SelectMany(x => x.MemberNames));

        /// <summary>
        /// Errors that have been found in this collection of <see cref="ValidationResult"/>s.
        /// The errors are separated by <see cref="Environment.NewLine"/>.
        /// </summary>
        public string Errors { get; set; }

        /// <summary>
        /// Checks if there are any errors in the collection.
        /// </summary>
        /// <param name="valid">
        /// <see cref="Validator.TryValidateProperty(object, ValidationContext, ICollection{ValidationResult})"/> result.
        /// </param>
        /// <returns></returns>
        public string CheckForErrors(bool valid)
        {
            Errors = valid
                ? ""
                : string.Join(Environment.NewLine, Results.Select(x => x.ErrorMessage));

            return Errors;
        }

        /// <summary>
        /// Adds validation result and checks for error afterwards.
        /// </summary>
        /// <param name="errorMessage">Error message for the validation.</param>
        /// <param name="member">Member that is checked.</param>
        /// <param name="valid">Result of the validation.</param>
        public void AddValidationResult(string errorMessage, string member, bool valid)
        {
            Results.Add(new ValidationResult(errorMessage, new string[] { member }));

            CheckForErrors(valid);
        }

        public override string ToString() => $"{Member}: {Errors}";
    }
}
