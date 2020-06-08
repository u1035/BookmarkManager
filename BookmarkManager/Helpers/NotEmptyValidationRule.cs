using System;
using System.Globalization;
using System.Windows.Controls;

namespace BookmarkManager.Helpers
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var strValue = Convert.ToString(value);

            return string.IsNullOrEmpty(strValue) 
                ? new ValidationResult(false, Properties.Resources.EmptyFieldError) 
                : ValidationResult.ValidResult;
        }
    }
}
