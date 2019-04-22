namespace FuncyStyleInCSharp.Models
{
    public class TestCase
    {
        public TestCase(string input, string expected)
        {
            this.Input = input;
            this.Expected = expected;
            this.Actual = null;
            this.State = TestCaseState.Todo;
            this.Info = "";
            this.Duration = -1;
        }
        public string Input { get; }
        public string Expected { get; }
        public string Actual { get; set; }
        public TestCaseState State { get; set; }
        public string Info { get; set; }
        public long Duration { get; set; }

    }

    public enum TestCaseState
    {
        Todo,
        Correct,
        Incorrect,
        Error,
        Tle
    }
}