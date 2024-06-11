namespace SyntaxeDotNet
{
    internal class SyntaxeRogueFunctions
    {
        /// <summary>
        /// Generate random key
        /// </summary>
        /// <returns>Random key</returns>
        public static string GetRandomKey() => Guid.NewGuid().ToString().Replace("-", "")[..20];

        /// <summary>
        /// Adjust regular expression pattern with start and end match constraint
        /// </summary>
        /// <returns>Adjusted pattern</returns>
        public static string Regexify(string regex, bool matchStart, bool matchEnd)
            => $@"{(matchStart ? "^" : "")}{regex}{(matchEnd ? "$" : "")}";
    }
}
