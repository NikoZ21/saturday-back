using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Saturday_Back.Features.Schedules.Validations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class EnsureAfterAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;
        public EnsureAfterAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext context)
        {
            var otherProp = context.ObjectType.GetProperty(_otherProperty,
                       BindingFlags.Public | BindingFlags.Instance);

            if (otherProp == null)
            {
                return new ValidationResult($"Unknown property '{_otherProperty}'.");
            }

            var otherValue = otherProp.GetValue(context.ObjectInstance);
            if (value is int current && otherValue is int other && current <= other)
            {
                return new ValidationResult(ErrorMessage ??
                   $"{context.DisplayName} must be greater than {_otherProperty}.");
            }

            return ValidationResult.Success!;
        }
    }
}