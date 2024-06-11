using System.Text.Json;
using System.Text.RegularExpressions;

namespace SyntaxeDotNet
{
    internal class SyntaxeEngine
    {
        private ISyntaxeEngineStateConfig Holder;

        public SyntaxeEngine() => Holder = new ISyntaxeEngineStateConfig();

        /// <summary>
        /// Filter operations
        /// </summary>
        /// <param name="schema">Query schema</param>
        /// <returns>Filtered schema with processed operations</returns>
        private async Task<string> FilterOperations(string schema)
        {
            var operationString = string.Empty;

            // Check schema context
            if (schema.Trim().StartsWith("["))
            {
                Holder.Context = "root";
                schema = @$"{{ rootPropertyIdentifier }} {schema}";
            }

            // Extract possible operations from schema
            var filtered = Regex.Replace(schema, Patterns.General!.NewLine, "");
            filtered = Regex.Replace(filtered, Patterns.Operations!.PropertyOp, (match) =>
            {
                // Detect property operations
                var indexOfDelimiter = match.ToString().IndexOf("[");
                var prop = match.ToString()[..indexOfDelimiter];
                var instrs = Regex.Replace(match.ToString()[indexOfDelimiter..], Patterns.General.OpList, "]*^*[").Split("]*^*[");
                var name = @$"*instr-p:id_{SyntaxeRogueFunctions.GetRandomKey()}";
                var propertyCondition = "and";
                var operationsList = new List<string>();
                instrs.ToList().ForEach((string ins) =>
                {
                    // Check for [cond] operator
                    if (Regex.IsMatch(ins, @"cond:""(and|or)"))
                        propertyCondition = Regex.IsMatch(ins, @"cond:""and") ? propertyCondition : "or";
                    else
                        operationsList.Add(Regex.Replace(ins, SyntaxeRogueFunctions.Regexify(Patterns.General.Operation, true, true), string.Empty));
                });

                // define omission regex
                var omissionRegex = SyntaxeRogueFunctions.Regexify(Patterns.General.Omission, false, true);
                // Check omission operator
                var omissionStatus = Regex.IsMatch(prop, omissionRegex);

                // Add property and info to state
                Holder.PropertyOps!.Add(name, new IPropertyOperationConfig
                {
                    Property = Regex.Replace(prop, omissionRegex, string.Empty),
                    Operation = operationsList.ToArray(),
                    Condition = propertyCondition,
                    Omit = omissionStatus
                });

                return $"{name}";
            });
            filtered = Regex.Replace(filtered, SyntaxeRogueFunctions.Regexify(Patterns.Operations!.ObjectOp, false, true), (match) =>
            {
                // Detect root object operations
                var indexOfDelimiter = match.ToString().IndexOf("[");
                var instrs = Regex.Replace(match.ToString()[indexOfDelimiter..], Patterns.General.OpList, "]*^*[").Split("]*^*[");
                var operationsList = new List<string>();
                instrs.ToList().ForEach((string ins) =>
                {
                    // Check for [cond] operator
                    if (Regex.IsMatch(ins, @"cond:""(and|or)"))
                        Holder.Condition = Regex.IsMatch(ins, @"cond:""and") ? Holder.Condition : "or";
                    else if (Regex.IsMatch(ins, @"mode:""(and|or)"))
                        Holder.Mode = Regex.IsMatch(ins, @"mode:""and") ? Holder.Mode : "or";
                    else
                        operationsList.Add(Regex.Replace(ins, SyntaxeRogueFunctions.Regexify(Patterns.General.Operation, true, true), string.Empty));
                });

                // Pass root operations
                Holder.RootOp = operationsList.ToArray();

                return "}";
            });
            filtered = Regex.Replace(filtered, Patterns.Operations!.ObjectOp, (match) =>
            {
                // Detect inner object operations
                var indexOfDelimiter = match.ToString().IndexOf("[");
                var instrs = Regex.Replace(match.ToString()[indexOfDelimiter..], Patterns.General.OpList, "]*^*[").Split("]*^*[");
                var name = @$"*instr-o:id_{SyntaxeRogueFunctions.GetRandomKey()}";
                var propertyCondition = "and";
                var propertyMode = "and";
                var operationsList = new List<string>();
                instrs.ToList().ForEach((string ins) =>
                {
                    // Check for [cond] operator
                    if (Regex.IsMatch(ins, @"cond:""(and|or)"))
                        propertyCondition = Regex.IsMatch(ins, @"cond:""and") ? propertyCondition : "or";
                    else if (Regex.IsMatch(ins, @"mode:""(and|or)"))
                        propertyMode = Regex.IsMatch(ins, @"mode:""and") ? propertyMode : "or";
                    else
                        operationsList.Add(Regex.Replace(ins, SyntaxeRogueFunctions.Regexify(Patterns.General.Operation, true, true), string.Empty));
                });

                // Add object and info to state
                Holder.ObjectOps!.Add(name, new IObjectOperationConfig
                {
                    Operation = operationsList.ToArray(),
                    Condition = propertyCondition,
                    Mode = propertyMode
                });

                return $"}} {name}";
            });

            return await Task.FromResult(filtered);
        }

        /// <summary>
        /// Filter schema
        /// </summary>
        /// <param name="schema">Query schema</param>
        /// <returns>Status and processed schema</returns>
        public async Task<IFilterSchemaResponse> FilterSchema(string schema)
        {
            try
            {
                // Filter operations
                var filtered = await FilterOperations(schema);

                // Prepare schema for JSON
                var resolve = Regex.Replace(filtered, Patterns.General!.NewLine, "");
                resolve = Regex.Replace(resolve, Patterns.Schema!.CommaAndSpace, " ");
                resolve = Regex.Replace(resolve, Patterns.Schema!.ObjectProperty, (match) => $@"""{match}""");
                resolve = Regex.Replace(resolve, Patterns.Schema!.SpaceAndBrace, (match) =>
                {
                    try
                    {
                        return new Dictionary<string, string>{
                                { @""" """, @""":1, """ },
                                { @""" {",  @""": {" },
                                { @"""{",   @""": {" },
                                { @""" }",  @""":1 }" },
                                { @"""}",   @""":1 }" }
                        }[match.ToString()];
                    }
                    catch
                    {
                        return string.Empty;
                    }
                });
                resolve = Regex.Replace(resolve, Patterns.Schema!.BraceAndSpace, @"}, """);

                var jsonHandler = new SyntaxeJsonHandler();
                var convertedSchema = jsonHandler.ParseJson(resolve);

                return new IFilterSchemaResponse
                {
                    Status = true,
                    Schema = convertedSchema
                };
            }
            catch (Exception ex)
            {
                return new IFilterSchemaResponse
                {
                    Status = false,
                    Error = ex.Message
                };
            }
        }

        /// <summary>
        /// /// Clean up and extract data
        /// </summary>
        /// <param name="context">Context config object containing data, schema and status</param>
        /// <returns></returns>
        public async Task<dynamic> WalkThroughHandler(IWalkThroughContextConfig context)
        {
            // Check if no schema
            if (context.Schema == null)
                return null!;

            try
            {
                // Detect need to convert data
                var subject = (context.Data.GetType().Name == "String")
                    ? (Holder.Context != "root" ? JsonSerializer.Deserialize(context.Data) : context.Data)
                    : context.Data;

                // Process the schema and subject
                var result = (Holder.Context == "json")
                    ? (await schemaWalkThrough(new ISchemaWalkThroughContextConfig
                    {
                        Schema = context.Schema,
                        Subject = subject
                    })).Result
                    : subject;

                // Check if root operations available
                if (Holder.RootOp!.Length > 0)
                {
                    // Create new random name
                    var name = @$"*instr-p:id_{SyntaxeRogueFunctions.GetRandomKey()}";

                    // Pass property name and operations into holder
                    Holder.PropertyOps!.Add(name, new IPropertyOperationConfig
                    {
                        Property = Holder.RootKey,
                        Operation = Holder.RootOp,
                        Condition = Holder.Condition
                    });

                    // Compute status
                    var operationStatus = await schemaWalkThrough(new ISchemaWalkThroughContextConfig
                    {
                        Schema = new Dictionary<string, object>
                        {
                            { name, 1 }
                        },
                        Subject = new Dictionary<string, dynamic>
                        {
                            { Holder.RootKey, result }
                        }
                    });
                    result = (operationStatus.SchemaPass == true)
                        ? operationStatus.Result![Holder.RootKey]
                        : (operationStatus.Result![Holder.RootKey].GetType().IsArray ? new dynamic[] {} : new Dictionary<string, object>());
                }

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToString()}] {{0}}", ex.ToString());
                return null!;
            }
        }

        /// <summary>
        /// Sweep through and extract data
        /// </summary>
        /// <param name="context">Context config object containing data, schema and mode</param>
        /// <returns></returns>
        public async Task<ISchemaWalkthroughResult> schemaWalkThrough(ISchemaWalkThroughContextConfig context)
        {
            // Define stores
            dynamic result = new Dictionary<string, dynamic>();
            var schemaPass = true;
            var schemaPassSet = new HashSet<bool>();
            dynamic filtered = null!;

            // Check if array
            if (context.Subject!.GetType().IsArray)
            {
                // Change result to array
                result = new List<dynamic>[] {};
                // Loop through data array
                foreach (var item in context.Subject)
                {
                    var line = await schemaWalkThrough(new ISchemaWalkThroughContextConfig
                    {
                        Schema = context.Schema,
                        Subject = context.Subject,
                        Mode = context.Mode
                    });
                    // Check if this line should be added to results
                    if (line.SchemaPass == true)
                        result.Add(line.Result);
                }
            } else
            {

            }

            return await Task.FromResult(new ISchemaWalkthroughResult
            {
                Result = result,
                SchemaPass = true
            });
        }
    }
}
