using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncyStyleInCSharp.Solvers
{
    public class FunctionSolver
    {
        public static Func<int, int> Fibonacci => i => i <= 2 ? 1 : Fibonacci(i - 1) + Fibonacci(i - 2);

        // Fibonacci implementation for "large" numbers
        public static Func<int, long> FibonacciLarge => i => FibonacciWithMemoization(i, new Dictionary<long, long>());

        //Recursive way with memoization
        public static long FibonacciWithMemoization(long i, Dictionary<long, long> cache) => cache.ContainsKey(i)
            ? cache[i]
            : cache[i] = (i < 2 ? i : FibonacciWithMemoization(i - 1, cache) + FibonacciWithMemoization(i - 2, cache));

        // Alternative way with aggregate
        public static Func<int, long> FibonacciLarge2 => i => Enumerable.Range(2, i).Aggregate(
            new Dictionary<int, long>() { { 0, 0 }, { 1, 1 } }
            , (acc, cur) => { acc[cur] = acc[cur - 1] + acc[cur - 2]; return acc; }
            , acc => acc[i]);

        //AzErTy -> yTrEzA
        public static Func<string, string> ToReverse =>
            s => s.ToCharArray()
            .Reverse()
            .Aggregate(new StringBuilder(), (acc, cur) => acc.Append(cur), acc => acc.ToString());

        //hello world -> HelloWorld
        //hEllO wOrLd -> HelloWorld
        public static Func<string, string> ToPascalCase =>
            s => s.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(sub => sub.Substring(0, 1).ToUpper() + sub.Substring(1).ToLower())
            .Aggregate(new StringBuilder(), (acc, cur) => acc.Append(cur), acc => acc.ToString());

        //hello world -> helloWorld
        //hEllO wOrLd -> helloWorld
        public static Func<string, string> ToCamelCase =>
            s => ToPascalCase(s).Substring(0, 1).ToLower().Trim() + ToPascalCase(s).Substring(1).Trim();

        //hello world -> hhheeellllllooo wwwooorrrlllddd
        public static Func<string, string> Repeat3Times => FunctionUtils.RepeatFactory(3);

        //hello world -> DlrowOlleh
        public static Func<string, string> ToReverseAndPascalCase => s => FunctionUtils.Pipe(s, ToReverse, ToPascalCase);

        //hello world -> dlrowOlleh
        public static Func<string, string> ToReverseAndCamelCase => s => FunctionUtils.Pipe(s, ToReverse, ToCamelCase);

        //hello world -> dddddlllllrrrrrooooowwwww     ooooollllleeeeehhhhh
        public static Func<string, string> ToReverseAndRepeat5Times => s => FunctionUtils.Pipe(s, ToReverse, FunctionUtils.RepeatFactory(5));

        // sum of comma separated minutes:seconds, result in seconds (including "seconds" unit)
        // "1:23" => "83 seconds"
        // "3:51,4:29,3:24" => "704 seconds"
        public static Func<string, string> SumOfDurations =>
            s => s.Split(",")
                    .Select(iv => TimeSpan.Parse("0:" + iv).TotalSeconds)
                    .Sum()
                    .ToString("# 'seconds'");

        // sum of pages (including "pages" unit)
        // "1,2,3" => "3 pages"
        // "1,3-6,9" => "6 pages" == (1+4+1)
        public static Func<string, string> SumOfPages =>
            s => s.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(iv => iv.Split("-"))
                    .Select(iv => 1 + int.Parse(iv.Last()) - int.Parse(iv.First()))
                    .Sum()
                    .ToString("0 'pages'");

        // count occurrences of each pizza order by count, then by name
        // "Pizza2,Pizza1" => "1 Pizza1, 1 Pizza2"
        // "Pepperoni,BBQ Chicken,Diavola,BBQ Chicken,Cheeseburger,Cheeseburger,BBQ Chicken" => "3 BBQ Chicken, 2 Cheesburger, 1 Diavola, 1 Pepperoni"
        public static Func<string, string> CountPizzas =>
            s => s.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .GroupBy(x => x)
                    .OrderByDescending(grp => grp.Count())
                    .ThenBy(grp => grp.Key)
                    .Select((grp, ix) => (ix > 0 ? ", " : "") + grp.Count() + " " + grp.Key)
                    .Aggregate(new StringBuilder(), (acc, p) => acc.Append(p), acc => acc.ToString());

        // Schedule the next round in the contest: 1th vs nth player, 2nd vs (n-1)th player, ...
        // There will be an even amount of players
        // "1,2" => "1-2"
        // "A,B,C,D" => "A-D,B-C"
        // "Djokovic,Nadal,Federer,Thiem,Zverev,Anderson,Tsitsipas,Nishikori" => "Djokovic-Nishikori,Nadal-Tsitsipas,Federer-Anderson,Thiem-Zverev"
        public static Func<string, string> CrossFinal =>
            s => s.Split(",", StringSplitOptions.RemoveEmptyEntries)
               .Pipe((IEnumerable<string> players) =>
                    players
                        .Take(players.Count() / 2)
                        .Zip(players.Reverse(), (string a, string b) => a + "-" + b)
               ).Aggregate(new StringBuilder()
                            , (acc, cur) => acc.Append(acc.Length == 0 ? "" : ",").Append(cur)
                            , acc => acc.ToString())
            ;

        // validate if a sequence of braces is correct (and make lisp programmers happy)
        // there should be as many opening braces '(' as closing braces ')'. At no time there can be a closing brace without a preceding opening brace
        // ()(())() => true
        // (()() => false
        // )()( => false
        public static Func<string, bool> ValidBraces =>
            s => s.Aggregate(new { cur = 0, min = 0 }
                , (acc, x) => new { cur = acc.cur + (x == '(' ? 1 : -1), min = Math.Min(acc.min, acc.cur) }
                , acc => acc.cur == 0 && acc.min == 0);
    }
    public static class FunctionUtils
    {
        /// <summary>
        /// Calls each function passing the result of the previous function as the input for the next one.
        /// The first parameter being passed is the value argument
        /// </summary>
        /// <typeparam name="TValue">The type returned</typeparam>
        /// <param name="value">The first parameter</param>
        /// <param name="functions">The list of function to execute</param>
        /// <returns>The resulting value</returns>
        public static TValue Pipe<TValue>(this TValue value, params Func<TValue, TValue>[] functions) => functions.Aggregate(value, (acc, f) => f(acc));

        /// <summary>
        /// Return a Repeat function who repeats n times each letter of the string
        /// </summary>
        /// <param name="n">The number of times that we repeat each character</param>
        /// <returns>The function</returns>
        public static Func<string, string> RepeatFactory(int n) =>
            s => s.Aggregate("", (acc, c) => acc + new string(c, n));
    }
}
