namespace SyntaxeDotNet
{
    public class ISyntaxeConfig
    {
        public dynamic? Data { get; set; }
        public dynamic? Schema { get; set; }
    }

    public class Syntaxe
    {
        private dynamic QueryData = null!;
        private dynamic QuerySchema = null!;
        public bool Success = false;
        public string Error = string.Empty;

        public Syntaxe() => Init(null!);
        public Syntaxe(ISyntaxeConfig config) => Init(config);

        private void Init(ISyntaxeConfig config)
        {
            if (config != null)
            {
                QueryData = config!.Data!;
                QuerySchema = config!.Schema!;
            }

            Success = false;
            Error = string.Empty;
        }

        public Syntaxe Schema(string schema)
        {
            if (!string.IsNullOrEmpty(schema))
                QuerySchema = schema!;

            return this;
        }

        public Syntaxe Data(dynamic data)
        {
            if (data != null)
                QueryData = data!;

            return this;
        }

        public async Task<dynamic> Query() => await QueryDelegte(null!);
        
        public async Task<dynamic> Query(ISyntaxeConfig config) => await QueryDelegte(config);

        public async Task<dynamic> QueryDelegte(ISyntaxeConfig config)
        {
            try
            {
                Init(config);

                if (QueryData == null)
                    Error = "'Data' is invalid.";
                else if (QuerySchema == null)
                    Error = "'Schema' is invalid.";

                if (!string.IsNullOrEmpty(Error))
                    return QueryData!;

                var engine = new SyntaxeEngine();

                IFilterSchemaResponse filtered = await engine.FilterSchema(QuerySchema);

                if (filtered.Status == true)
                {
                    var result = await engine.WalkThroughHandler(new IWalkThroughContextConfig
                    {
                        Data = QueryData!,
                        Schema = filtered.Schema,
                        Status = filtered.Status
                    });

                    if (result == null)
                    {
                        Error = "Query failed. Check your schema and try again.";
                        return QueryData!;
                    }

                    Success = true;
                    return result;
                } else
                {
                    Error = filtered.Error!;
                    return await Task.FromResult(QueryData);
                }
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                return QueryData;
            }
        }
    }
}