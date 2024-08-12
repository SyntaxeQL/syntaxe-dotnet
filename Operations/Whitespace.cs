using System.Text.RegularExpressions;

namespace SyntaxeDotNet
{
    internal static class Whitespace
    {
        public static dynamic Remove(dynamic value, string leftSide, string filtered)
        {
            filtered = (filtered == leftSide) ? string.Empty : filtered;
            return Regex.Replace(value.ToString(), SyntaxeRogueFunctions.Regexify(Patterns.General!.Ws, false, false), filtered ?? string.Empty);
        }

        public static dynamic RemoveExtra(dynamic value, string leftSide, string filtered)
        {
            filtered = (filtered == leftSide) ? string.Empty : filtered;
            return Regex.Replace(value.ToString(), SyntaxeRogueFunctions.Regexify(Patterns.General!.Raws, false, false), filtered ?? string.Empty);
        }
    }
}
