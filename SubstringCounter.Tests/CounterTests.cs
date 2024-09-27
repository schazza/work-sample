using Xunit;

namespace SubstringCounter.Tests
{
    public class CounterTests : TestBase
    {
        [Theory]
        [InlineData("file", 1)]
        [InlineData("file\nfile", 2)]
        [InlineData("filefilefile", 3)]
        public void ProgramShouldReturnCorrectCountOfSubstringsInFile(string fileContents, int count)
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
    }
}