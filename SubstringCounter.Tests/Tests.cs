



namespace SubstringCounter.Tests
{
    public class Tests
    {
        private readonly Program _consoleApp;
        private readonly StringWriter _output;
        private string? _argument;

        public Tests()
        {
            _consoleApp = new Program();
            _output = new StringWriter();
            Console.SetOut(_output);
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfNoArgsGiven()
        {
            GivenNoArgument();
            WhenRunningProgram();
            ThenOutputShouldBe("Usage: SubstringCounter <filename>");
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

        private static void GivenFileWithContents(string fileName, string contents)
        {
            File.WriteAllText(fileName, contents);
        }

        private void GivenNoArgument()
        {
        }

        private void GivenArgument(string argument)
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