using System.Diagnostics.CodeAnalysis;

namespace SubstringCounter
{
    internal class FilePathValidator
    {
        internal static bool Validate(string[] args, [NotNullWhen(true)] out string? filePath, [NotNullWhen(false)] out string? errorMessage)
        {
            if (NoValidArgumentGiven(args) || MultipleArgumentsGiven(args) )
            {
                errorMessage = "Usage: SubstringCounter <file path>";
                filePath = null;
                return false;
            }

            filePath = args[0];
            if (FileNameIsEmpty(filePath)){
                errorMessage = "Given file has no name";
                return false;
            }
            if (!FileExists(filePath))
            {
                errorMessage = "File not found or file format invalid";
                return false;
            }
            if (!IsFileAccessible(filePath))
            {
                errorMessage = "File not accessible for reading";
                return false;
            }
            errorMessage = null;
            return true;
        }

        private static bool NoValidArgumentGiven(string[] args) => args.Length == 0 || string.IsNullOrWhiteSpace(args[0]);
        private static bool MultipleArgumentsGiven(string[] args) => args.Length > 1;
        private static bool FileNameIsEmpty(string filePath) => string.IsNullOrWhiteSpace(Path.GetFileNameWithoutExtension(filePath));
        private static bool FileExists(string filePath) => File.Exists(filePath);
        private static bool IsFileAccessible(string filePath)
        {
            try
            {
                using FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }

}
