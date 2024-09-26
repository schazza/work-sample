namespace SubstringCounter
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: SubstringCounter <filename>");
                return;
            }
            var fileContents = ReadFileContents(args[0]);
            PrintFileNameOccurrences(args[0], fileContents);
        }

        private static string ReadFileContents(string path)
        {
            using StreamReader reader = new(path);
            return reader.ReadToEnd();
        }

        private static void PrintFileNameOccurrences(string path, string fileContents)
        {
            var fileName = path.Split('.')[0];
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
