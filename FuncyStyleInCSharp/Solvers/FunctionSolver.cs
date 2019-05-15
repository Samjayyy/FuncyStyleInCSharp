using System;
using System.Linq;
using System.Text;

namespace FuncyStyleInCSharp.Solvers
{
    public class FunctionSolver
    {
        public static Func<int, int> Fibonacci => i => i <= 2 ? 1 : Fibonacci(i - 1) + Fibonacci(i - 2);

        // Fibonacci implementation for "large" numbers
        public static Func<int, long> FibonacciLarge => i =>
        {
            if (i <= 2)
            {
                return 1;
            }
            long a1 = 1;
            long a2 = 1;
            while (i-- > 2)
            {
                long tmp = a2;
                a2 += a1;
                a1 = tmp;
            }
            return a2;
        };

        //AzErTy -> yTrEzA
        public static Func<string, string> ToReverse => s => throw new NotImplementedException("TODO");

        //hello world -> HelloWorld
        //hEllO wOrLd -> HelloWorld
        public static Func<string, string> ToPascalCase => s => throw new NotImplementedException("TODO");

        //hello world -> helloWorld
        //hEllO wOrLd -> helloWorld
        public static Func<string, string> ToCamelCase => s => throw new NotImplementedException("TODO");

        //hello world -> hhheeellllllooo wwwooorrrlllddd
        public static Func<string, string> Repeat3Times => s => throw new NotImplementedException("TODO");

        //hello world -> DlrowOlleh
        public static Func<string, string> ToReverseAndPascalCase => s => throw new NotImplementedException("TODO");

        //hello world -> dlrowOlleh
        public static Func<string, string> ToReverseAndCamelCase => s => throw new NotImplementedException("TODO");

        //hello world -> dddddlllllrrrrrooooowwwww     ooooollllleeeeehhhhh
        public static Func<string, string> ToReverseAndRepeat5Times => s => throw new NotImplementedException("TODO");

        // sum of comma separated minutes:seconds, result in seconds (including "seconds" unit)
        // "1:23" => "83 seconds"
        // "3:51,4:29,3:24" => "704 seconds"
        public static Func<string, string> SumOfDurations => s => throw new NotImplementedException("TODO");

        // sum of pages (including "pages" unit)
        // "1,2,3" => "3 pages"
        // "1,3-6,9" => "6 pages" == (1+4+1)
        public static Func<string, string> SumOfPages => s => throw new NotImplementedException("TODO");

        // count occurrences of each pizza order by count, then by name
        // "Pizza2,Pizza1" => "1 Pizza1, 1 Pizza2"
        // "Pepperoni,BBQ Chicken,Diavola,BBQ Chicken,Cheeseburger,Cheeseburger,BBQ Chicken" => "3 BBQ Chicken, 2 Cheesburger, 1 Diavola, 1 Pepperoni"
        public static Func<string, string> CountPizzas => s => throw new NotImplementedException("TODO");

        // Schedule the next round in the contest: 1th vs nth player, 2nd vs (n-1)th player, ...
        // There will be an even amount of players
        // "1,2" => "1-2"
        // "A,B,C,D" => "A-D,B-C"
        // "Djokovic,Nadal,Federer,Thiem,Zverev,Anderson,Tsitsipas,Nishikori" => "Djokovic-Nishikori,Nadal-Tsitsipas,Federer-Anderson,Thiem-Zverev"
        public static Func<string, string> CrossFinal => s => throw new NotImplementedException("TODO");

        // validate if a sequence of braces is correct (and make lisp programmers happy)
        // there should be as many opening braces '(' as closing braces ')'. At no time there can be a closing brace without a preceding opening brace
        // ()(())() => true
        // (()() => false
        // )()( => false
        public static Func<string, bool> ValidBraces => s => throw new NotImplementedException("TODO");
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
        public static TValue Pipe<TValue>(this TValue value, params Func<TValue, TValue>[] functions) => throw new NotImplementedException("TODO");

        /// <summary>
        /// Return a Repeat function who repeats n times each letter of the string
        /// </summary>
        /// <param name="n">The number of times that we repeat each character</param>
        /// <returns>The function</returns>
        public static Func<string, string> RepeatFactory(int n) => throw new NotImplementedException("TODO");
    }
}
