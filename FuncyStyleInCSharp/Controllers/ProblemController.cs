using FuncyStyleInCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FuncyStyleInCSharp.Controllers
{
    public class ProblemController : Controller
    {
        public IActionResult Index()
        {
            return View(ProblemSet.Problems.Values);
        }
     
        public IActionResult Detail(string name)
        {
            if (string.IsNullOrWhiteSpace(name)
                || !ProblemSet.Problems.ContainsKey(name))
            {
                return Redirect("/");
            }
            return View(ProblemSet.Problems[name]);
        }

    }
}
