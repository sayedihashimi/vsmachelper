using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;
using System.ComponentModel.DataAnnotations;

namespace VsmacHelper.Shared {
    public class VsmacVersionValidator : IOptionValidator {
        public ValidationResult GetValidationResult(CommandOption option, ValidationContext context) {
            if (!option.HasValue()) return ValidationResult.Success;

            var val = option.Value();

            if (val != "7.0" && val != "8.0") {
                return new ValidationResult($"The value for --{option.LongName} must be either '7.0' or '8.0'");
            }

            return ValidationResult.Success;
        }
    }
}
