using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using static UserStoryGenerator.Model.Settings;
using static UserStoryGenerator.View.TriStateTreeView;

namespace UserStoryGenerator.Model
{
    public class TreeNodeExConverter : JsonConverter<TreeNodeEx>
    {
        public override TreeNodeEx? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if( reader.TokenType != JsonTokenType.StartObject )
            {
                throw new JsonException();
            }

            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            JsonElement root = doc.RootElement;


            if( !root.TryGetProperty("IssueType", out var issueTypeProperty) )
            {
                throw new JsonException("Missing 'IssueType' property.");
            }
            string issueType = issueTypeProperty.GetString()!;

            // Create a new instance of TreeNodeEx directly.
            TreeNodeEx issue = issueType switch
            {
                //case "Sub-task":
                JiraIssueType.Sub_task => new TreeNodeExSubTask(),
                JiraIssueType.Test => new TreeNodeExTest(),
                JiraIssueType.Task => new TreeNodeExTask(),
                JiraIssueType.Story or "Bug" or "Epic" => new TreeNodeEx(),
                _ => new TreeNodeEx(),
            };
            foreach( var property in root.EnumerateObject() )
            {
                var propertyInfo = issue.GetType().GetProperty(property.Name);
                if( propertyInfo == null ) continue;
                //Logger.Info(property.Name);

                if( property.Name == "Component" && issue is TreeNodeExTask taskIssue )
                {
                    var componentJson = property.Value.GetRawText();
                    taskIssue.Component = JsonSerializer.Deserialize<Component>(componentJson, options);
                }
                else
                {
                    var value = JsonSerializer.Deserialize(property.Value.GetRawText(), propertyInfo.PropertyType, options);
                    propertyInfo.SetValue(issue, value);
                }

            }

            issue.Name = issue.Key.ToString();
            issue.Text = issue.Summary;
            issue.ToolTipText = issue.IssueType;
            issue.ImageIndex = Utilities.IssueUtilities.GetImageIndex(issue.IssueType);


            if( root.TryGetProperty("Subtasks", out JsonElement subtasksElement) && subtasksElement.ValueKind == JsonValueKind.Object )
            {
                TreeNodeExSubTasks subtasksCollectionNode = new();
                issue.Nodes.Add(subtasksCollectionNode);

                foreach( JsonProperty property in subtasksElement.EnumerateObject() )
                {
                    if( property.Value.ValueKind == JsonValueKind.Object )
                    {
                        // Use the default JsonSerializer to deserialize the child objects,
                        // which will invoke this same converter recursively for each valid node.
                        TreeNodeEx? subTask = JsonSerializer.Deserialize<TreeNodeEx>(property.Value.GetRawText(), options);
                        if( subTask != null )
                        {
                            subtasksCollectionNode.Nodes.Add(subTask);
                        }
                    }
                }
            }

            if( root.TryGetProperty("LinkedIssues", out JsonElement linkedIssuesElement) && linkedIssuesElement.ValueKind == JsonValueKind.Object )
            {
                TreeNodeExLinkedIssues linkedIssuesCollectionNode = new();
                issue.Nodes.Add(linkedIssuesCollectionNode);

                foreach( JsonProperty property in linkedIssuesElement.EnumerateObject() )
                {
                    if( property.Value.ValueKind == JsonValueKind.Object )
                    {
                        TreeNodeEx? linkedIssue = JsonSerializer.Deserialize<TreeNodeEx>(property.Value.GetRawText(), options);
                        if( linkedIssue != null )
                        {
                            linkedIssuesCollectionNode.Nodes.Add(linkedIssue);
                        }
                    }
                }
            }

            return issue;
            //
        }

        public override void Write(Utf8JsonWriter writer, TreeNodeEx value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Create a temporary options instance for recursive calls that does not include this converter.
            // This is crucial to prevent infinite recursion when a nested TreeNodeEx is encountered.
            var nonRecursiveOptions = new JsonSerializerOptions(options); // Copy all settings
                                                                          // Remove this specific converter instance from the temporary options.
                                                                          // We create a new list of converters without this converter, then clear and add them.
            List<JsonConverter> filteredConverters = nonRecursiveOptions.Converters
                .Where(c => c.GetType() != typeof(TreeNodeExConverter))
                .ToList();
            nonRecursiveOptions.Converters.Clear();
            foreach( var converter in filteredConverters )
            {
                nonRecursiveOptions.Converters.Add(converter);
            }

            // NEW: Get properties declared ONLY in TreeNodeEx, and the 'Children' property from TreeNode
            PropertyInfo[] declaredProperties = typeof(TreeNodeEx).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            List<PropertyInfo> propertiesToProcess = [.. declaredProperties];

            PropertyInfo? childrenProperty = typeof(TreeNode).GetProperty("Nodes", BindingFlags.Public | BindingFlags.Instance);
            if( childrenProperty != null && !propertiesToProcess.Contains(childrenProperty) )
                propertiesToProcess.Add(childrenProperty);

            if( value is TreeNodeExTask )
            {
                PropertyInfo? componentProperty = typeof(TreeNodeExTask).GetProperty("Component", BindingFlags.Public | BindingFlags.Instance);
                if( componentProperty != null )
                    propertiesToProcess.Add(componentProperty);
            }

            foreach( PropertyInfo property in propertiesToProcess ) // Iterate over the selected properties
            {
                if( property.CanRead ) // Ensure the property can be read
                {
                    // Skip IntPtr properties during serialization
                    if( property.PropertyType == typeof(IntPtr) )
                    {
                        //Console.WriteLine($"Skipping serialization of IntPtr property: {property.Name}");
                        continue; // Skip this property
                    }

                    if( property.Name == "Nodes" && property.PropertyType == typeof(System.Windows.Forms.TreeNodeCollection) )
                    {
                        object? propertyValue = property.GetValue(value); // Get the property's current value
                        if( propertyValue is System.Windows.Forms.TreeNodeCollection collection )
                        {
                            foreach( TreeNode item in collection )
                            {
                                //Logger.Info(item.GetType());

                                TreeNodeEx? treeNodeEx;// = item as TreeNodeEx;

                                if( item is TreeNodeExTask task )
                                    treeNodeEx = task;
                                else if( item is TreeNodeExTest test )
                                    treeNodeEx = test;
                                else if( item is TreeNodeExSubTask subTask )
                                    treeNodeEx = subTask;
                                else
                                    treeNodeEx = item as TreeNodeEx;

                                if( treeNodeEx != null )
                                {
                                    writer.WritePropertyName(treeNodeEx.Text);
                                    Write(writer, treeNodeEx, options);// recursive here
                                }
                                else
                                {
                                    writer.WritePropertyName("Description");
                                    propertyValue = item.Text; // Get the property's current value
                                    Type propertyType = item.Text.GetType();
                                    JsonSerializer.Serialize(writer, propertyValue, propertyType, options);//typeof(String)
                                }

                            }
                            //
                        }
                        //

                    }
                    else
                    {
                        //if( property.Name.Equals("Component") )
                        //{
                        //    writer.WritePropertyName(property.Name);
                        //    object? propertyValue = property.GetValue(value); // Get the property's current value
                        //    Type propertyType = property.PropertyType;

                        //    // Delegate the serialization of the property's value back to the default serializer.
                        //    // Use nonRecursiveOptions to prevent infinite recursion if propertyValue is TreeNodeEx
                        //    // or a collection containing TreeNodeEx.
                        //    JsonSerializer.Serialize(writer, propertyValue, propertyType, nonRecursiveOptions);
                        //}
                        //else
                        {
                            writer.WritePropertyName(property.Name);
                            object? propertyValue = property.GetValue(value); // Get the property's current value
                            Type propertyType = property.PropertyType;

                            // Delegate the serialization of the property's value back to the default serializer.
                            // Use nonRecursiveOptions to prevent infinite recursion if propertyValue is TreeNodeEx
                            // or a collection containing TreeNodeEx.
                            JsonSerializer.Serialize(writer, propertyValue, propertyType, nonRecursiveOptions);
                        }
                    }
                }
            }

            writer.WriteEndObject();
        }
    }

    public class IssueConverter : JsonConverter<List<Issue>>
    {
        public override List<Issue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if( reader.TokenType != JsonTokenType.StartArray )
            {
                throw new JsonException("Expected start of array for Issues.");
            }

            var issues = new List<Issue>();

            while( reader.Read() )
            {
                if( reader.TokenType == JsonTokenType.EndArray )
                {
                    // Exit the loop cleanly when the EndArray token is found
                    break;
                }

                if( reader.TokenType != JsonTokenType.StartObject )
                {
                    // Handle unexpected tokens
                    throw new JsonException("Expected start of object for an Issue.");
                }

                issues.Add(ReadSingleIssue(ref reader, options));
            }

            return issues;

        }

        private Issue ReadSingleIssue(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var rootElement = jsonDocument.RootElement;

            if( !rootElement.TryGetProperty("IssueType", out var issueTypeProperty) )
            {
                throw new JsonException("Missing 'IssueType' property.");
            }
            string issueType = issueTypeProperty.GetString()!;
            Issue issue = issueType switch
            {
                //case "Sub-task":
                JiraIssueType.Sub_task => new SubTask(),
                JiraIssueType.Test => new TestTask(),
                JiraIssueType.Task => new TaskIssue(),
                JiraIssueType.Story or "Bug" or "Epic" => new Issue(),
                _ => new Issue(),
            };
            foreach( var property in rootElement.EnumerateObject() )
            {
                var propertyInfo = issue.GetType().GetProperty(property.Name);
                if( propertyInfo == null ) continue;

                if( ( property.Name == "Subtasks" || property.Name == "LinkedIssues" ) && property.Value.ValueKind == JsonValueKind.Array )
                {
                    List<Issue>? nestedIssues = DeserializeNestedIssues(property.Value, options);

                    // Correctly cast the list based on the property name
                    if( property.Name == "Subtasks" )
                    {
                        try
                        {
                            var subtasksOnly = nestedIssues?.OfType<SubTask>().ToList();
                            propertyInfo.SetValue(issue, subtasksOnly);

                            // Cast to List<SubTask>
                            //propertyInfo.SetValue(issue, nestedIssues?.Cast<SubTask>().ToList());
                        }
                        catch( Exception )
                        {
                        }
                    }
                    else
                    {
                        // Cast to List<Issue>
                        propertyInfo.SetValue(issue, nestedIssues);
                    }

                }
                else if( property.Name == "Component" && issue is TaskIssue taskIssue )
                {
                    var componentJson = property.Value.GetRawText();
                    taskIssue.Component = JsonSerializer.Deserialize<Component>(componentJson, options);
                }
                else
                {
                    var value = JsonSerializer.Deserialize(property.Value.GetRawText(), propertyInfo.PropertyType, options);
                    propertyInfo.SetValue(issue, value);
                }
            }

            return issue;
        }

        private List<Issue>? DeserializeNestedIssues(JsonElement element, JsonSerializerOptions options)
        {
            if( element.ValueKind != JsonValueKind.Array )
            {
                return null;
            }

            var nestedIssues = new List<Issue>();
            foreach( var nestedElement in element.EnumerateArray() )
            {
                if( nestedElement.ValueKind == JsonValueKind.Object )
                {
                    // Create a new Utf8JsonReader for each nested object and recursively call the ReadSingleIssue method
                    var nestedReader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(nestedElement.GetRawText()));
                    nestedIssues.Add(ReadSingleIssue(ref nestedReader, options));
                }
            }

            return nestedIssues;
        }

        public override void Write(Utf8JsonWriter writer, List<Issue> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach( var issue in value )
            {
                writer.WriteStartObject();
                // Get all properties of the specific issue type
                var properties = issue.GetType().GetProperties();
                foreach( var property in properties )
                {
                    var propertyValue = property.GetValue(issue);
                    if( propertyValue == null ) continue;

                    writer.WritePropertyName(property.Name);

                    // Manually handle nested collections
                    if( property.Name == "Subtasks" && propertyValue is List<SubTask> subtasks )
                    {
                        Write(writer, subtasks.Cast<Issue>().ToList(), options);
                    }
                    else if( property.Name == "LinkedIssues" && propertyValue is List<Issue> linkedIssues )
                    {
                        Write(writer, linkedIssues, options);
                    }
                    else
                    {
                        // Use the default serializer for all other properties
                        JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
                    }
                }
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

    }
}
