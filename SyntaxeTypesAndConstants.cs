namespace SyntaxeDotNet
{
    internal class IPatternsGeneral
    {
        public string NewLine { get; set; } = @"(\\n|\n|\\r|\r)";
        public string OpList { get; set; } = @"\](\s*)\[";
        public string Operation { get; set; } = @"\[|\]";
        public string Quotes { get; set; } = @"(`|""|')|(`|""|')";
        public string Raws { get; set; } = @" {2,}";
        public string Ws { get; set; } = @" ";
        public string NonTimeXter { get; set; } = @"[^\d:]";
        public string NonDigit { get; set; } = @"[^\d]";
        public string NonDecimal { get; set; } = @"[^\d\.]";
        public string NonAlphabet { get; set; } = @"[^a-z]";
        public string NonSign { get; set; } = @"[^-\+]";
        public string Omission { get; set; } = @"\?";
    }

    internal class IPatternsOperations
    {
        public string PropertyOp { get; set; } = @"(((\w+)(\s*)(\??))((\s*)\[(\s*)(\w+)(\s*)(:*)(\s*)(\'([\w\+\s\(\):]*|\s{1,})\'|\""([\w\+\s\(\):]*|\s{1,})\""|(\[[\""\w\+\s\(\):\"",]+\])|(\""[-\d\w\s]+\"")|(\[(\s*)\/(\^?)(\w+)(\$?)\/(\w*)(\,(\s*)\/(\^?)(\w+)(\$?)\/(\w*))*(\s*)\])|(\[(\s*)\""[\w\/]*\""(\,(\s*)\""[\w\/]*\"")+(\s*)\](\s*))|(\w+)|((\/)(\S+)(\/)(\w*)(\s*))|(\S+))\])+)";
        public string ObjectOp { get; set; } = @"(((\})(\s*))((\s*)\[(\w+)(\s*)(:*)(\s*)((\[[\""\w\+\s\(\):\"",]+\])|\""(\w+)\""|(\w+)|((\+|\-){1}\w+))\])+)";
    }

    internal class IPatternsSchema
    {
        public string CommaAndSpace { get; set; } = @"(,)|(\s{2,})";
        public string ObjectProperty { get; set; } = @"(((\w+)(\-|\.)*(\w+)(\??))|(\*instr-p:((\w+)(\-|\.)*(\w+)))|(\*instr-o:id_(\w+)))";
        public string SpaceAndBrace { get; set; } = @"("" "")|(("" |"")\{)|(("" |"")\})";
        public string BraceAndSpace { get; set; } = @"\}( ""|"")";
    }

    internal class ISyntaxeEngineStateConfig
    {
        public Dictionary<string, IPropertyOperationConfig>? PropertyOps { get; set; } = new Dictionary<string, IPropertyOperationConfig>();
        public dynamic? ObjectOps { get; set; } = new Dictionary<string, dynamic>();
        public string[]? RootOp { get; set; } = null!;
        public string? RootKey { get; set; } = "root";
        public string? Context { get; set; } = "json";
        public string? Mode { get; set; } = "and";
        public string? Condition { get; set; } = "and";
        public int[]? DefaultDate { get; set; } = { 1991, 6, 1 };
    }

    internal class IPropertyOperationConfig
    {
        public string? Property { get; set; }
        public string[]? Operation { get; set; }
        public string? Condition { get; set; }
        public bool? Omit { get; set; }
    }

    internal class IObjectOperationConfig
    {
        public string[]? Operation { get; set; }
        public string? Condition { get; set; }
        public string? Mode { get; set; }
    }

    internal class IFilterSchemaResponse
    {
        public bool? Status { get; set; }
        public Dictionary<string, object>? Schema { get; set; }
        public string? Error { get; set; }
    }

    internal class IWalkThroughContextConfig
    {
        public dynamic Data { get; set; }
        public dynamic? Status { get; set; }
        public dynamic? Schema { get; set; }
    }

    internal class ISchemaWalkThroughContextConfig
    {
        public dynamic? Schema { get; set; }
        public dynamic? Subject { get; set; }
        public dynamic? Mode { get; set; }
    }

    internal class ISchemaWalkthroughResult
    {
        public bool? SchemaPass { get; set; }
        public dynamic? Result { get; set; }
    }
}
