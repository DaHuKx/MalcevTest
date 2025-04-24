using StringCompressor.Validators;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCompressor
{
    public static class Compressor
    {
        private static CompressValidator _compressValidator;
        private static DecompressValidator _decompressValidator;

        static Compressor()
        {
            _compressValidator = new CompressValidator();
            _decompressValidator = new DecompressValidator();
        }

        public static string Compress(string str)
        {
            StringBuilder sb = new StringBuilder();

            var validationResult = _compressValidator.Validate(str);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(e => sb.Append(e.ToString()));
                return sb.ToString();
            }

            char currentSymbol = str.First();
            int counter = 1;

            foreach (char c in str.Skip(1))
            {
                if (currentSymbol == c)
                {
                    counter++;
                    continue;
                }

                sb.Append($"{currentSymbol}{(counter != 1 ? counter : string.Empty)}");
                currentSymbol = c;
                counter = 1;
            }

            sb.Append($"{currentSymbol}{(counter != 1 ? counter : string.Empty)}");

            return sb.ToString();
        }

        public static string Decompress(string str)
        {
            StringBuilder sb = new StringBuilder();

            var validationResult = _decompressValidator.Validate(str);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(e => sb.Append(e.ToString()));
                return sb.ToString();
            }

            Regex regex = new Regex("[a-z]{1}[0-9]*");

            var compresses = regex.Matches(str).ToList();

            char symbol;
            int count;
            foreach (var compress in compresses)
            {
                if (compress.Value.Length == 1)
                {
                    sb.Append(compress.Value);
                    continue;
                }

                symbol = compress.Value.First();
                count = int.Parse(compress.Value.Substring(1));

                sb.Append(string.Concat(Enumerable.Repeat(symbol, count)));
            }

            return sb.ToString();
        }
    }
}
