using System.Security.AccessControl;
using Xunit;

namespace SubstringCounter.Tests
{
    public class Tests : IDisposable
    {
        private readonly StringWriter _output;
        private string? _argument;
        private string? _filePath;

        public Tests()
        {
            _output = new StringWriter();
            Console.SetOut(_output);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (_filePath != null)
            {
                File.Delete(_filePath);
            }
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfNoArgsGiven()
        {
            GivenNoArgument();
            WhenRunningProgram();
            ThenOutputShouldBe("Usage: SubstringCounter <file path>");
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfEmptyArgGiven()
        {
            GivenArgument("");
            WhenRunningProgram();
            ThenOutputShouldBe("Usage: SubstringCounter <file path>");
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfNullArgGiven()
        {
            GivenArgument(null);
            WhenRunningProgram();
            ThenOutputShouldBe("Usage: SubstringCounter <file path>");
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfFilePathIncludesInvalidChar()
        {
            GivenArgument("invalid|char.txt");
            WhenRunningProgram();
            ThenOutputShouldBe("File path includes invalid character");
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfFileInaccessible()
        {
            if (IsRunningInCI())
            {
                return;
            }
            GivenArgument("inaccessible.txt");
            GivenFileWithContents("inaccessible.txt", "file");
            GivenFileIsInaccessible("inaccessible.txt");
            WhenRunningProgram();
            ThenOutputShouldBe("File not accessible for reading");

            static bool IsRunningInCI()
            {
                return Environment.GetEnvironmentVariable("CI") == "true";
            }
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfFileNotFound()
        {
            GivenArgument("nonexistantfile.txt");
            GivenFileWithContents("file.txt", "file");
            WhenRunningProgram();
            ThenOutputShouldBe("File not found or file format invalid");
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfFileFormatInvalid()
        {
            GivenArgument("invalidfileformat");
            WhenRunningProgram();
            ThenOutputShouldBe("File not found or file format invalid");
        }

        [Fact]
        public void ProgramShouldReturnCountIfFileGivenAsArg()
        {
            GivenFileWithContents("file.txt", "file");
            GivenArgument("file.txt");
            WhenRunningProgram();
            ThenOutputShouldBe("Found 1");
        }

        [Theory]
        [InlineData("file", 1)]
        [InlineData("file\nfile", 2)]
        [InlineData("filefilefile", 3)]
        public void ProgramShouldReturnCorrectCountOfSubstringInFile(string fileContents, int count)
        {
            GivenFileWithContents("file.txt", fileContents);
            GivenArgument("file.txt");
            WhenRunningProgram();
            ThenOutputShouldBe($"Found {count}");
        }

        [Fact]
        public void ProgramShouldNotCountOverlappingSubstrings()
        {
            GivenFileWithContents("fifi.txt", "fifififi");
            GivenArgument("fifi.txt");
            WhenRunningProgram();
            ThenOutputShouldBe("Found 2");
        }

        private void GivenFileWithContents(string filePath, string contents)
        {
            File.WriteAllText(filePath, contents);
            _filePath = filePath;
        }

        private static void GivenFileIsInaccessible(string filePath)
        {
            FileSecurity fileSecurity = new FileSecurity();
            fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.Read, AccessControlType.Deny));
            new FileInfo(filePath).SetAccessControl(fileSecurity);
        }

        private static void GivenNoArgument()
        {
        }

        private void GivenArgument(string? argument)
        {
            _argument = argument;
        }

        private void WhenRunningProgram()
        {
            var arguments = _argument == null ? [] : new[] { _argument };
            Program.Main(arguments);
        }

        private void ThenOutputShouldBe(string outputMessage)
        {
            var consoleOutput = _output.ToString().Trim();
            Assert.Equal(outputMessage, consoleOutput);
        }
    }
}