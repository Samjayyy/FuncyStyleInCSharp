using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FuncyStyleInCSharp.Models
{
    public class ProblemBuilder<TCase, TSol> : IProblemBuilder
    {
        private List<TCase> _cases;
        private List<TSol> _expecteds;
        private Func<TCase, TSol> _solveFunc;
        private Func<TSol, TSol, bool> _correctChecker;

        public ProblemBuilder()
        {
            _cases = new List<TCase>();
            _expecteds = new List<TSol>();
            _correctChecker = (a, b) => a == null ? b == null : a.Equals(b);
        }

        public string Name { get; private set; }

        public ProblemBuilder<TCase, TSol> WithName(string name)
        {
            this.Name = name;
            return this;
        }

        public ProblemBuilder<TCase, TSol> WithTestCase(TCase testCase, TSol expected)
        {
            this._cases.Add(testCase);
            this._expecteds.Add(expected);
            return this;
        }

        public ProblemBuilder<TCase, TSol> WithChecker(Func<TSol, TSol, bool> checkFunc)
        {
            this._correctChecker = checkFunc;
            return this;
        }

        public ProblemBuilder<TCase, TSol> WithSolver(Func<TCase, TSol> solver)
        {
            this._solveFunc = solver;
            return this;
        }

        public Problem Solve()
        {
            var problem = new Problem(this.Name, this._cases.Count);
            var tasks = new Task[this._cases.Count];
            for(int i=0;i< this._cases.Count; i++)
            {
                var tc = new TestCase(this._cases[i].ToString(), this._expecteds[i].ToString());
                problem.TestCases[i] = tc;
                var fix = i;
                tasks[i] = new Task(() => Solve(tc, fix));
                tasks[i].Start();
            }
            Task.WaitAll(tasks);
            return problem;
        }

        private void Solve(TestCase tc, int i)
        {
            try
            {
                if(this._solveFunc == null)
                {
                    throw new NotImplementedException("Solve func not implemented or not set for problem");
                }
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                TSol res = this._solveFunc(this._cases[i]);
                stopwatch.Stop();
                tc.Duration = stopwatch.ElapsedMilliseconds;
                tc.Actual = res.ToString();
                tc.State = this._correctChecker(this._expecteds[i], res) ? TestCaseState.Correct : TestCaseState.Incorrect;
            }
            catch (NotImplementedException ex)
            {
                tc.State = TestCaseState.Todo;
                tc.Info = ex.Message;
            }
            catch (Exception ex)
            {
                // gotta catch em all
                tc.State = TestCaseState.Error;
                tc.Info = ex.Message;
            }
        }

    }
}