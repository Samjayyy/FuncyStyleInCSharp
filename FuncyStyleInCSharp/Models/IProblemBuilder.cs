namespace FuncyStyleInCSharp.Models
{
    public interface IProblemBuilder
    {
        string Name { get; }

        Problem Solve();
    }
}