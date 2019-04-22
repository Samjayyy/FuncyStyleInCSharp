using FuncyStyleInCSharp.Solvers;
using System.Collections.Generic;
using System.Linq;

namespace FuncyStyleInCSharp.Models
{
    public static class ProblemSet
    {
        private static IDictionary<string, Problem> _problems = null;

        public static IDictionary<string, Problem> Problems
        {
            get
            {
                var arr = new IProblemBuilder[]{
                    new ProblemBuilder<string, string>()
                        .WithName("Reverse")
                        .WithSolver(FunctionSolver.ToReverse)
                        .WithTestCase("AzErTy", "yTrEzA")
                        .WithTestCase("poCsM", "MsCop")
                        .WithTestCase("efil4azzip", "pizza4life")
                        .WithTestCase("Microsoft Community", "ytinummoC tfosorciM")
                    , new ProblemBuilder<string, string>()
                        .WithName("Pascal Case")
                        .WithSolver(FunctionSolver.ToPascalCase)
                        .WithTestCase("hello world", "HelloWorld")
                        .WithTestCase("hEllO wOrLd", "HelloWorld")
                        .WithTestCase("Hello wOrlD", "HelloWorld")
                        .WithTestCase("hEllO WOrLd", "HelloWorld")
                        .WithTestCase("Microsoft Community", "MicrosoftCommunity")
                    , new ProblemBuilder<string, string>()
                        .WithName("Camel Case")
                        .WithSolver(FunctionSolver.ToCamelCase)
                        .WithTestCase("hello world", "helloWorld")
                        .WithTestCase("hEllO wOrLd", "helloWorld")
                        .WithTestCase("Hello worlD", "helloWorld")
                        .WithTestCase("HElLo WOrLd", "helloWorld")
                        .WithTestCase("Microsoft Community", "microsoftCommunity")
                    , new ProblemBuilder<string, string>()
                        .WithName("Repeat 3 Times")
                        .WithSolver(FunctionSolver.Repeat3Times)
                        .WithTestCase("hello world", "hhheeellllllooo   wwwooorrrlllddd")
                        .WithTestCase("Op zen Limburgs", "OOOppp   zzzeeennn   LLLiiimmmbbbuuurrrgggsss")
                        .WithTestCase("Microsoft Community", "MMMiiicccrrrooosssooofffttt   CCCooommmmmmuuunnniiitttyyy")
                    , new ProblemBuilder<string, string>()
                        .WithName("Reverse and Pascal Case")
                        .WithSolver(FunctionSolver.ToReverseAndPascalCase)
                        .WithTestCase("hello world", "DlrowOlleh")
                        .WithTestCase("hEllO wOrLd", "DlrowOlleh")
                        .WithTestCase("Hello wOrlD", "DlrowOlleh")
                        .WithTestCase("hEllO WOrLd", "DlrowOlleh")
                        .WithTestCase("Microsoft Community", "YtinummocTfosorcim")
                    , new ProblemBuilder<string, string>()
                        .WithName("Reverse and Camel Case")
                        .WithSolver(FunctionSolver.ToReverseAndCamelCase)
                        .WithTestCase("hello world", "dlrowOlleh")
                        .WithTestCase("hEllO wOrLd", "dlrowOlleh")
                        .WithTestCase("Hello wOrlD", "dlrowOlleh")
                        .WithTestCase("hEllO WOrLd", "dlrowOlleh")
                        .WithTestCase("Microsoft Community", "ytinummocTfosorcim")
                    , new ProblemBuilder<string, string>()
                        .WithName("Reverse and Repeat 5 times")
                        .WithSolver(FunctionSolver.ToReverseAndRepeat5Times)
                        .WithTestCase("hello world", "dddddlllllrrrrrooooowwwww     ooooolllllllllleeeeehhhhh")
                        .WithTestCase("Microsoft Community", "yyyyytttttiiiiinnnnnuuuuummmmmmmmmmoooooCCCCC     tttttfffffooooosssssooooorrrrrccccciiiiiMMMMM")
                    , new ProblemBuilder<string, string>()
                        .WithName("Sum of Durations")
                        .WithSolver(FunctionSolver.SumOfDurations)
                        .WithTestCase("1:23", "83 seconds")
                        .WithTestCase("0:01,0:02,0:03", "6 seconds")
                        .WithTestCase("3:51,4:29,3:24", "704 seconds")
                        .WithTestCase("3:27,2:43,3:24,3:51,4:12,4:29,3:14,4:46,3:25,4:52", "2303 seconds")
                    , new ProblemBuilder<string, string>()
                        .WithName("Sum of Pages")
                        .WithSolver(FunctionSolver.SumOfPages)
                        .WithTestCase("", "0 pages")
                        .WithTestCase("1", "1 pages")
                        .WithTestCase("1,2,3", "3 pages")
                        .WithTestCase("1-3", "3 pages")
                        .WithTestCase("1,3-6,9", "6 pages")
                        .WithTestCase("1-3,10-13", "7 pages")
                        .WithTestCase("2,5,7-10,11,17-18", "9 pages")
                    , new ProblemBuilder<string, string>()
                        .WithName("Pizza Count")
                        .WithSolver(FunctionSolver.CountPizzas)
                        .WithTestCase("Pizza2,Pizza1", "1 Pizza1, 1 Pizza2")
                        .WithTestCase("Pepperoni,BBQ Chicken,Diavola,BBQ Chicken,Cheeseburger,Cheeseburger,BBQ Chicken", "3 BBQ Chicken, 2 Cheeseburger, 1 Diavola, 1 Pepperoni")
                        .WithTestCase("a,c,e,g,b,d,f,h,e,b", "2 b, 2 e, 1 a, 1 c, 1 d, 1 f, 1 g, 1 h")
                    };
                return _problems = arr
                    .Select(pb => pb.Solve())
                    .ToDictionary(pb => pb.LinkName);
            }
        }
    }
}
