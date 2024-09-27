using Xunit;

namespace SubstringCounter.Tests
{
    [Collection("Non-Parallel Collection")]
    public abstract class TestBase : IDisposable
    {
        internal readonly StringWriter _output;
        internal string[]? _arguments;
        internal string? _filePath;
        internal string? _folderPath;

        public TestBase()
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
            if (_folderPath != null)
            {
                Directory.Delete(_folderPath);
            }
        }

        internal static void GivenNoArgument()
        {
        }

        internal void GivenArgument(string? argument)
        {
            _arguments = argument == null ? null : [argument];
        }

        internal void GivenArguments(string[] arguments)
        {
            _arguments = arguments;
        }

        internal void GivenFolder(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
            _folderPath = folderPath;
        }

        internal void GivenFileWithContents(string filePath, string contents)
        {
            File.WriteAllText(filePath, contents);
            _filePath = filePath;
        }

        internal void WhenRunningProgram()
        {
            var arguments = _arguments ?? [];
            Program.Main(arguments);
        }

        internal void ThenOutputShouldBe(string expectedOutputMessage)
        {
            var consoleOutput = _output.ToString().Trim();
            Assert.Equal(expectedOutputMessage, consoleOutput);
        }
    }
}
