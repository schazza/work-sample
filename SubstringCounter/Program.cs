using System.Text.RegularExpressions;

namespace SubstringCounter
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            if (!FilePathValidator.Validate(args, out string? filePath, out string? errorMessage))
            {
                Console.WriteLine(errorMessage);
                return;
            }
            var fileContents = File.ReadAllText(filePath);
            PrintFileNameOccurrences(filePath, fileContents);
        }

        private static void PrintFileNameOccurrences(string filePath, string fileContents)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var count = CountSubstringOccurrences(fileContents, fileName);
            Console.WriteLine("Found " + count);
        }

        private static int CountSubstringOccurrences(string contents, string substring)
        {
            if (string.IsNullOrEmpty(substring))
                return 0;

            var regex = new Regex(Regex.Escape(substring), RegexOptions.IgnoreCase);
            return regex.Matches(contents).Count;
        }

    }
}
