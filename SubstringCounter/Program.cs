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
            var fileContents = ReadFileContents(filePath);
            PrintFileNameOccurrences(args[0], fileContents);
        }

        private static string ReadFileContents(string filePath)
        {
            using StreamReader reader = new(filePath);
            return reader.ReadToEnd();
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

            int count = 0;
            int index = 0;
            while ((index = contents.IndexOf(substring, index)) != -1)
            {
                count++;
                index += substring.Length;
            }
            return count;
        }

    }
}
