namespace SyntaxeDotNet
{
    internal class Patterns
    {
        public static IPatternsGeneral? General { get; set; } = new IPatternsGeneral();
        public static IPatternsOperations? Operations { get; set; } = new IPatternsOperations();
        public static IPatternsSchema? Schema { get; set; } = new IPatternsSchema();
    }

    internal class HayStackOperations
    {
        public static List<string> All { get; set; } = new List<string>
        {
            "in", "nin", "ini", "nini", "sin", "snin", "dtin", "dtnin", "dtinrange", "dtninrange", "dtmin", "dtmnin", "dtminrange", "dtmninrange",
            "yin", "ynin", "yinrange", "yninrange", "min", "mnin", "minrange", "mninrange", "din", "dnin", "dinrange", "dninrange", "dwin", "dwnin", "dwinrange", "dwninrange",
            "hin", "hnin", "hinrange", "hninrange", "minin", "minnin", "mininrange", "minninrange",
            "tin", "tnin", "tinrange", "tninrange", "agoin", "btw"
        };
        public static List<string> NumberBound { get; set; } = new List<string>
        {
            "sin", "snin", "yin", "ynin", "btw"
        };
    }

    internal class SchemaOperators
    {
        public const string Alias = "as";
        public const string RemoveExtraWhitespace = "rew";
        public const string RemoveWhitespace = "rw";
        public const string EqualTo = "eq";
        public const string EqualToCaseInsensitive = "eqi";
        public const string NotEqualTo = "ne";
        public const string NotEqualToCaseInsensitive = "nei";
        public const string GreaterThan = "gt";
        public const string GreaterThanOrEqualTo = "gte";
        public const string LessThan = "lt";
        public const string LessThanOrEqualTo = "lte";
        public const string NotNull = "nn";
        public const string In = "in";
        public const string NotIn = "nin";
        public const string InCaseInsensitive = "ini";
        public const string NotInCaseInsensitive = "nini";
        public const string RegexEqualTo = "regex";
        public const string RegexNotEqualTo = "regexne";
        public const string RegexIn = "regexin";
        public const string RegexNotIn = "regexnin";
        public const string Size = "size";
        public const string SizeEqualTo = "seq";
        public const string SizeNotEqualTo = "sne";
        public const string SizeGreaterThan = "sgt";
        public const string SizeLessThan = "slt";
        public const string SizeGreaterThanOrEqualTo = "sgte";
        public const string SizeLessThanOrEqualTo = "slte";
        public const string SizeIn = "sin";
        public const string SizeNotIn = "snin";
        public const string DateEqualTo = "dteq";
        public const string DateNotEqualTo = "dtne";
        public const string DateGreaterThan = "dtgt";
        public const string DateLessThan = "dtlt";
        public const string DateGreaterThanOrEqualTo = "dtgte";
        public const string DateLessThanOrEqualTo = "dtlte";
        public const string DateIn = "dtin";
        public const string DateNotIn = "dtnin";
        public const string DateInRange = "dtinrange";
        public const string DateNotInRange = "dtninrange";
        public const string DateTimeEqualTo = "dtmeq";
        public const string DateTimeNotEqualTo = "dtmne";
        public const string DateTimeGreaterThan = "dtmgt";
        public const string DateTimeLessThan = "dtmlt";
        public const string DateTimeGreaterThanOrEqualTo = "dtmgte";
        public const string DateTimeLessThanOrEqualTo = "dtmlte";
        public const string DateTimeIn = "dtmin";
        public const string DateTimeNotIn = "dtmnin";
        public const string DateTimeInRange = "dtminrange";
        public const string DateTimeNotInRange = "dtmninrange";
        public const string YearEqualTo = "yeq";
        public const string YearNotEqualTo = "yne";
        public const string YearGreaterThan = "ygt";
        public const string YearLessThan = "ylt";
        public const string YearGreaterThanOrEqualTo = "ygte";
        public const string YearLessThanOrEqualTo = "ylte";
        public const string YearIn = "yin";
        public const string YearNotIn = "ynin";
        public const string YearInRange = "yinrange";
        public const string YearNotInRange = "yninrange";
        public const string MonthEqualTo = "meq";
        public const string MonthNotEqualTo = "mne";
        public const string MonthGreaterThan = "mgt";
        public const string MonthLessThan = "mlt";
        public const string MonthGreaterThanOrEqualTo = "mgte";
        public const string MonthLessThanOrEqualTo = "mlte";
        public const string MonthIn = "min";
        public const string MonthNotIn = "mnin";
        public const string MonthInRange = "minrange";
        public const string MonthNotInRange = "mninrange";
        public const string Today = "today";
        public const string DayEqualTo = "deq";
        public const string DayNotEqualTo = "dne";
        public const string DayGreaterThan = "dgt";
        public const string DayLessThan = "dlt";
        public const string DayGreaterThanOrEqualTo = "dgte";
        public const string DayLessThanOrEqualTo = "dlte";
        public const string DayIn = "din";
        public const string DayNotIn = "dnin";
        public const string DayInRange = "dinrange";
        public const string DayNotInRange = "dninrange";
        public const string DayOfWeekEqualTo = "dweq";
        public const string DayOfWeekNotEqualTo = "dwne";
        public const string DayOfWeekGreaterThan = "dwgt";
        public const string DayOfWeekLessThan = "dwlt";
        public const string DayOfWeekGreaterThanOrEqualTo = "dwgte";
        public const string DayOfWeekLessThanOrEqualTo = "dwlte";
        public const string DayOfWeekIn = "dwin";
        public const string DayOfWeekNotIn = "dwnin";
        public const string DayOfWeekInRange = "dwinrange";
        public const string DayOfWeekNotInRange = "dwninrange";
        public const string HourEqualTo = "heq";
        public const string HourNotEqualTo = "hne";
        public const string HourGreaterThan = "hgt";
        public const string HourLessThan = "hlt";
        public const string HourGreaterThanOrEqualTo = "hgte";
        public const string HourLessThanOrEqualTo = "hlte";
        public const string HourIn = "hin";
        public const string HourNotIn = "hnin";
        public const string HourInRange = "hinrange";
        public const string HourNotInRange = "hninrange";
        public const string MinuteEqualTo = "mineq";
        public const string MinuteNotEqualTo = "minne";
        public const string MinuteGreaterThan = "mingt";
        public const string MinuteLessThan = "minlt";
        public const string MinuteGreaterThanOrEqualTo = "mingte";
        public const string MinuteLessThanOrEqualTo = "minlte";
        public const string MinuteIn = "minin";
        public const string MinuteNotIn = "minnin";
        public const string MinuteInRange = "mininrange";
        public const string MinuteNotInRange = "minninrange";
        public const string TimeEqual = "teq";
        public const string TimeNotEqual = "tne";
        public const string TimeGreaterThan = "tgt";
        public const string TimeLessThan = "tlt";
        public const string TimeIn = "tin";
        public const string TimeNotIn = "tnin";
        public const string TimeInRange = "tinrange";
        public const string TimeNotInRange = "tninrange";
        public const string Ago = "ago";
        public const string AgoIn = "agoin";
        public const string First = "first";
        public const string Last = "last";
        public const string Between = "btw";
        public const string Distinct = "dist";
    }
}
