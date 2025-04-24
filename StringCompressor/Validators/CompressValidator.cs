using FluentValidation;
using System.Text.RegularExpressions;

namespace StringCompressor.Validators
{
    public class CompressValidator : AbstractValidator<string>
    {
        public CompressValidator()
        {
            RuleFor(x => x).NotEmpty()
                           .NotNull()
                           .Must(ContainsOnlyLatinLetters)
                           .WithMessage("Text must contains only latin lower letters.");
        }

        private bool ContainsOnlyLatinLetters(string arg)
        {
            Regex regex = new Regex("^[a-z]+$");

            return regex.IsMatch(arg);
        }
    }
}
