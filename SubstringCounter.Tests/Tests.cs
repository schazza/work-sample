



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
        public void ConsoleAppShouldExitWithMessageIfNoArgsGiven()
        {
            GivenNoArgument();
            WhenRunningConsoleApp();
            ThenOutputShouldBe("Usage: SubstringCounter <filename>");
        }

        [Fact]
        public void ConsoleAppShouldReturnCountIfFileGivenAsArg()
        {
            GivenFileWithContents("file.txt", "file");
            GivenArgument("file.txt");
            WhenRunningConsoleApp();
            ThenOutputShouldBe("Found 1");
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

        private void WhenRunningConsoleApp()
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