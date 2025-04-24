using FluentValidation;
using System.Text.RegularExpressions;

namespace StringCompressor.Validators
{
    public class DecompressValidator : AbstractValidator<string>
    {
        public DecompressValidator()
        {
            RuleFor(x => x).NotNull()
                           .NotEmpty()
                           .Must(ContainsLettersAndNumbers)
                                .WithMessage("Text must contains only lower letters and numbers")
                           .Must(x => char.IsLetter(x.First()))
                                .WithMessage("First symbol must be letter")
                           .Must(PairsStartsWithZero)
                                .WithMessage("Symbol can't have 0 or 1 count");
        }

        private bool PairsStartsWithZero(string arg)
        {
            Regex regex = new Regex("[a-z](0|1[a-z]|1$)");

            return !regex.IsMatch(arg);
        }

        private bool ContainsLettersAndNumbers(string arg)
        {
            Regex regex = new Regex("^[a-z0-9]+$");

            return regex.IsMatch(arg);
        }
    }
}
