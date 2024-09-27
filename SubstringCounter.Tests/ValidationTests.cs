using System.Security.AccessControl;
using Xunit;

namespace SubstringCounter.Tests
{
    public class ValidationTests : TestBase
    {
        [Fact]
        public void ProgramShouldExitWithMessageIfNoArgsGiven()
        {
            GivenNoArgument();
            WhenRunningProgram();
            ThenOutputShouldBe("Usage: SubstringCounter <file path>");
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfMultipleArgsGiven()
        {
            GivenArguments(["file.txt", "file2.txt"]);
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
        public void ProgramShouldExitWithMessageIfFileNameEmpty()
        {
            GivenArgument(".gitignore");
            WhenRunningProgram();
            ThenOutputShouldBe("Given file has no name");
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfFilePathIncludesInvalidChar()
        {
            GivenArgument("invalid|char.txt");
            WhenRunningProgram();
            ThenOutputShouldBe("File not found or file format invalid");
        }

        [Fact]
        public void ProgramShouldExitWithMessageIfFileInaccessible()
        {
            // Testing logic for making file inaccessible only available on Windows
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

        [Fact]
        public void ProgramShouldReturnCountIfFileWithPathGivenAsArg()
        {
            GivenFolder("subfolder.tests");
            GivenFileWithContents("subfolder.tests/file.txt", "file");
            GivenArgument("subfolder.tests/file.txt");
            WhenRunningProgram();
            ThenOutputShouldBe("Found 1");
        }

        private static void GivenFileIsInaccessible(string filePath)
        {
            FileSecurity fileSecurity = new FileSecurity();
            fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.Read, AccessControlType.Deny));
            new FileInfo(filePath).SetAccessControl(fileSecurity);
        }
    }
}