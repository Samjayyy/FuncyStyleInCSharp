using System.Linq;

namespace FuncyStyleInCSharp.Models
{
    public class Problem
    {

        public Problem(string name, int nrOfCases)
        {
            this.Name = name;
            TestCases = new TestCase[nrOfCases];
        }
        public string Name { get; }

        public string LinkName => Name.Replace(" ", "").ToLower();

        public TestCase[] TestCases { get; }

        public bool IsSolved => TestCases.All(tc => tc != null && tc.State == TestCaseState.Correct);

        public TestCaseState State {
            get {
                if (TestCases.Any(tc => tc.State == TestCaseState.Tle || tc.State == TestCaseState.Error || tc.State == TestCaseState.Incorrect))
                {
                    return TestCaseState.Incorrect;
                }
                if (TestCases.Any(tc => tc.State == TestCaseState.Todo))
                {
                    return TestCaseState.Todo;
                }
                return TestCaseState.Correct;
            }
        }
    }
}