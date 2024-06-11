namespace SyntaxeDotNet
{
    internal class Patterns
    {
        public static IPatternsGeneral? General { get; set; } = new IPatternsGeneral();
        public static IPatternsOperations? Operations { get; set; } = new IPatternsOperations();
        public static IPatternsSchema? Schema { get; set; } = new IPatternsSchema();
    }
}
