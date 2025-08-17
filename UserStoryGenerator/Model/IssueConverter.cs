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

            using( JsonDocument doc = JsonDocument.ParseValue(ref reader) )
            {
                JsonElement root = doc.RootElement;

                // Create a new instance of TreeNodeEx directly.
                //TreeNodeEx finalNode = new TreeNodeEx();

                if( !root.TryGetProperty("IssueType", out var issueTypeProperty) )
                {
                    throw new JsonException("Missing 'IssueType' property.");
                }
                string issueType = issueTypeProperty.GetString()!;

                TreeNodeEx issue;// = new();//treeNodeEx


                switch( issueType )
                {
                    //case "Sub-task":
                    case JiraIssueType.Sub_task:
                        issue = new TreeNodeExSubTask();
                        break;
                    case JiraIssueType.Test:
                        issue = new TreeNodeExTest();
                        break;
                    case JiraIssueType.Task:
                        issue = new TreeNodeExTask();
                        break;
                    case JiraIssueType.Story:
                    case "Bug":
                    case "Epic":
                        issue = new TreeNodeEx();
                        break;
                    default:
                        issue = new TreeNodeEx();
                        break;
                }

                foreach( var property in root.EnumerateObject() )
                {
                    var propertyInfo = issue.GetType().GetProperty(property.Name);
                    if( propertyInfo == null ) continue;
                    //Logger.Info(property.Name);

                    if( property.Name == "Component" && issue is TreeNodeExTask taskIssue )
                    {
                        var componentJson = property.Value.GetRawText();
                        taskIssue.Component = JsonSerializer.Deserialize<IssueData.Component>(componentJson, options);
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
                    TreeNodeExSubTasks subtasksCollectionNode = new TreeNodeExSubTasks();
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
                    TreeNodeExLinkedIssues linkedIssuesCollectionNode = new TreeNodeExLinkedIssues();
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

                /*
                // Manually populate the properties from the JsonElement.
                // This is the key change to prevent the stack overflow.
                if( root.TryGetProperty("Product", out JsonElement productElement) )
                {
                    finalNode.Product = productElement.GetString();
                }
                if( root.TryGetProperty("Summary", out JsonElement summaryElement) )
                {
                    finalNode.Summary = summaryElement.GetString();
                }
                if( root.TryGetProperty("IssueType", out JsonElement issueTypeElement) )
                {
                    finalNode.IssueType = issueTypeElement.GetString();
                }
                if( root.TryGetProperty("Description", out JsonElement descriptionElement) )
                {
                    finalNode.Description = descriptionElement.GetString();
                }
                if( root.TryGetProperty("Key", out JsonElement keyElement) )
                {
                    finalNode.Key = keyElement.GetInt64();
                }
                if( root.TryGetProperty("StoryPoints", out JsonElement storyPointsElement) && storyPointsElement.ValueKind != JsonValueKind.Null )
                {
                    finalNode.StoryPoints = storyPointsElement.GetUInt32();
                }
                if( root.TryGetProperty("OriginalEstimate", out JsonElement originalEstimateElement) && originalEstimateElement.ValueKind != JsonValueKind.Null )
                {
                    finalNode.OriginalEstimate = originalEstimateElement.GetSingle();
                }
                // Add other properties as needed.

                // Handle the specific collection nodes
                if( root.TryGetProperty("Subtasks", out JsonElement subtasksElement) && subtasksElement.ValueKind == JsonValueKind.Object )
                {
                    TreeNodeExSubTasks subtasksCollectionNode = new TreeNodeExSubTasks();
                    finalNode.Nodes.Add(subtasksCollectionNode);

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
                    TreeNodeExLinkedIssues linkedIssuesCollectionNode = new TreeNodeExLinkedIssues();
                    finalNode.Nodes.Add(linkedIssuesCollectionNode);

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
                */

                return issue;
            }
        }


        /*
        public TreeNodeEx Read9(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if( reader.TokenType != JsonTokenType.StartObject )
            {
                throw new JsonException("Expected StartObject token for TreeNode.");
            }

            TreeNodeEx node = ReadSingleIssue(ref reader, options);

            return node;
        }

        private TreeNodeEx ReadSingleIssue(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var rootElement = jsonDocument.RootElement;

            if( !rootElement.TryGetProperty("IssueType", out var issueTypeProperty) )
            {
                throw new JsonException("Missing 'IssueType' property.");
            }
            string issueType = issueTypeProperty.GetString()!;

            TreeNodeEx issue = new();//treeNodeEx


            switch( issueType )
            {
                //case "Sub-task":
                case JiraIssueType.Sub_task:
                    issue = new TreeNodeExSubTask();
                    break;
                case JiraIssueType.Test:
                    issue = new TreeNodeExTest();
                    break;
                case JiraIssueType.Task:
                    issue = new TreeNodeExTask();
                    break;
                case JiraIssueType.Story:
                case "Bug":
                case "Epic":
                    issue = new TreeNodeEx();
                    break;
                default:
                    issue = new TreeNodeEx();
                    break;
            }

            foreach( var property in rootElement.EnumerateObject() )
            {
                var propertyInfo = issue.GetType().GetProperty(property.Name);
                if( propertyInfo == null ) continue;

                if( ( property.Name == "Subtasks" || property.Name == "LinkedIssues" ) && property.Value.ValueKind == JsonValueKind.Array )
                {
                    List<TreeNodeEx>? nestedIssues = DeserializeNestedIssues(property.Value, options);

                    // Correctly cast the list based on the property name
                    if( property.Name == "Subtasks" )
                    {
                        try
                        {
                            var subtasksOnly = nestedIssues?.OfType<IssueData.SubTask>().ToList();
                            propertyInfo.SetValue(issue, subtasksOnly);

                            // Cast to List<SubTask>
                            //propertyInfo.SetValue(issue, nestedIssues?.Cast<IssueData.SubTask>().ToList());
                        }
                        catch( Exception )
                        {
                            //throw ex;
                        }
                    }
                    else
                    {
                        // Cast to List<Issue>
                        propertyInfo.SetValue(issue, nestedIssues);
                    }

                }
                else if( property.Name == "Component" && issue is TreeNodeExTask taskIssue )
                {
                    var componentJson = property.Value.GetRawText();
                    taskIssue.Component = JsonSerializer.Deserialize<IssueData.Component>(componentJson, options);
                }
                else
                {
                    var value = JsonSerializer.Deserialize(property.Value.GetRawText(), propertyInfo.PropertyType, options);
                    propertyInfo.SetValue(issue, value);
                }
            }

        fuck:
            JsonElement linkedIssues = rootElement.GetProperty("LinkedIssues");

            if( rootElement.TryGetProperty("LinkedIssues", out JsonElement linkedIssuesObject) )
            {
                var nestedIssues = DeserializeNestedIssues(linkedIssuesObject, options);

                // Now, you need to find the array property *inside* this object.
                // Let's assume the array property is named "issues".
                if( linkedIssuesObject.TryGetProperty("Product", out JsonElement issuesArray) )
                {
                    // Now you can safely iterate over the array.
                    foreach( JsonElement item in issuesArray.EnumerateArray() )
                    {
                        // Do something with each individual issue element.
                        // For example, get a property value from the 'issue' object:
                        string issueId = item.GetProperty("Summary").GetString();
                        Console.WriteLine($"Found issue ID: {issueId}");
                    }
                }
            }


            goto fuck;



            return issue;

        }

        private List<TreeNodeEx>? DeserializeNestedIssues(JsonElement element, JsonSerializerOptions options)
        {
            if( element.ValueKind != JsonValueKind.Array )
            {
                return null;
            }

            List<TreeNodeEx> nestedIssues = [];
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


        private List<TreeNode> DeserializeTreeNodeCollection(ref Utf8JsonReader reader, JsonSerializerOptions options, string propertyName)
        {
            List<TreeNode> nodesList = new List<TreeNode>();

            if( reader.TokenType == JsonTokenType.StartArray )
            {
                // Standard array of nodes
                while( reader.Read() && reader.TokenType != JsonTokenType.EndArray )
                {
                    TreeNode? node = JsonSerializer.Deserialize<TreeNode>(ref reader, options);
                    if( node != null ) nodesList.Add(node);
                }
            }
            else if( reader.TokenType == JsonTokenType.StartObject )
            {
                // If it's a StartObject, we assume it's a wrapper object containing a 'nodes' array
                // Consume the StartObject
                bool nodesFound = false;
                while( reader.Read() && reader.TokenType != JsonTokenType.EndObject )
                {
                    if( reader.TokenType == JsonTokenType.PropertyName )
                    {
                        string innerPropertyName = reader.GetString() ?? string.Empty;
                        reader.Read(); // Advance to inner property value

                        if( innerPropertyName.Equals("nodes", StringComparison.OrdinalIgnoreCase) ) // Look for "nodes" within the object
                        {
                            nodesFound = true;
                            // Now deserialize the actual array of nodes
                            if( reader.TokenType == JsonTokenType.StartArray )
                            {
                                while( reader.Read() && reader.TokenType != JsonTokenType.EndArray )
                                {
                                    TreeNode? node = JsonSerializer.Deserialize<TreeNode>(ref reader, options);
                                    if( node != null ) nodesList.Add(node);
                                }
                            }
                            else if( reader.TokenType == JsonTokenType.StartObject )
                            {
                                // If "nodes" is a single object, add it
                                TreeNode? node = JsonSerializer.Deserialize<TreeNode>(ref reader, options);
                                if( node != null ) nodesList.Add(node);
                            }
                            else if( reader.TokenType == JsonTokenType.Null )
                            {
                                // 'nodes' property is null, so the list remains empty
                            }
                            else
                            {
                                throw new JsonException($"Expected StartArray, StartObject, or Null token for '{innerPropertyName}' inside '{propertyName}'. Found {reader.TokenType}.");
                            }
                        }
                        else
                        {
                            reader.Skip(); // Skip other unexpected properties within the wrapper object
                        }
                    }
                }
                // If the object was consumed and no "nodes" property was found, the list will remain empty.
                // You might want to throw an error here if "nodes" is mandatory within the wrapper object.
                // if (!nodesFound) throw new JsonException($"'{propertyName}' object did not contain a 'nodes' property.");
            }
            else if( reader.TokenType == JsonTokenType.Null )
            {
                // Do nothing, list remains empty for null
            }
            else
            {
                throw new JsonException($"Expected StartArray, StartObject, or Null token for '{propertyName}'. Found {reader.TokenType}.");
            }
            return nodesList;
        }
        */


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
            List<PropertyInfo> propertiesToProcess = declaredProperties.ToList();

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
    public class DraggableNodeDataConverter : JsonConverter<DraggableNodeData>
    {
        public override DraggableNodeData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if( reader.TokenType != JsonTokenType.StartObject )
            {
                throw new JsonException("Expected StartObject token.");
            }

            var node = new DraggableNodeData();
            while( reader.Read() )
            {
                if( reader.TokenType == JsonTokenType.EndObject )
                {
                    return node;
                }

                if( reader.TokenType == JsonTokenType.PropertyName )
                {
                    string? propertyName = reader.GetString();
                    reader.Read(); // Advance to the property value

                    switch( propertyName )
                    {
                        case "Text":
                            node.Text = reader.GetString();
                            break;
                        case "TagJson":
                            node.TagJson = reader.GetString();
                            break;
                        case "Product":
                            node.Product = reader.GetString();
                            break;
                        case "Summary":
                            node.Summary = reader.GetString();
                            break;
                        case "IssueType":
                            node.IssueType = reader.GetString();
                            break;
                        case "Description":
                            node.Description = reader.GetString();
                            break;
                        case "Key":
                            node.Key = reader.GetInt64();
                            break;
                        case "StoryPoints":
                            node.StoryPoints = reader.TokenType == JsonTokenType.Null ? null : (uint?)reader.GetUInt32();
                            break;
                        case "OriginalEstimate":
                            node.OriginalEstimate = reader.TokenType == JsonTokenType.Null ? null : (float?)reader.GetSingle();
                            break;
                        case "Children":
                            if( reader.TokenType == JsonTokenType.StartArray )
                            {
                                // Recursively deserialize the children
                                node.Children = JsonSerializer.Deserialize<List<DraggableNodeData>>(ref reader, options) ?? new List<DraggableNodeData>();
                            }
                            break;
                    }
                }
            }

            throw new JsonException("EndObject token not found.");
        }

        const string SUBTASKS = "SubTasks";
        const string LINKEDISSUES = "LinkedIssues";

        public override void Write(Utf8JsonWriter writer, DraggableNodeData value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            PropertyInfo[] properties = typeof(DraggableNodeData).GetProperties();

            foreach( PropertyInfo property in properties )
            {
                if( property.Name == "Children" )
                {
                    continue; // Handle Children separately at the end
                }

                writer.WritePropertyName(property.Name);
                object? propertyValue = property.GetValue(value);
                Type propertyType = property.PropertyType;

                // Use the generic Serialize method for all properties
                // System.Text.Json will correctly handle nulls, primitives, etc.
                JsonSerializer.Serialize(writer, propertyValue, propertyType, options);
            }

            // Explicitly handle the 'Children' property, ensuring recursive serialization
            writer.WritePropertyName("Children");
            JsonSerializer.Serialize(writer, value.Children, options); // options is important here for recursion

            writer.WriteEndObject();
        }
    }


    public class DraggableNodeDataConverter0 : JsonConverter<DraggableNodeData>
    {
        public override DraggableNodeData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if( reader.TokenType != JsonTokenType.StartObject )
            {
                throw new JsonException("Expected StartObject token.");
            }

            var nodeData = new DraggableNodeData();

            while( reader.Read() )
            {
                if( reader.TokenType == JsonTokenType.EndObject )
                {
                    return nodeData;
                }

                if( reader.TokenType != JsonTokenType.PropertyName )
                {
                    throw new JsonException("Expected PropertyName token.");
                }

                string? propertyName = reader.GetString();
                reader.Read(); // Advance to the property value

                switch( propertyName.ToLowerInvariant() )
                {
                    //case "id":
                    //    nodeData.Id = reader.GetString();
                    //    break;
                    //case "name":
                    //    nodeData.Name = reader.GetString();
                    //    break;
                    //case "x":
                    //    nodeData.X = reader.GetDouble();
                    //    break;
                    //case "y":
                    //    nodeData.Y = reader.GetDouble();
                    //    break;
                    //case "properties":
                    //    // Deserialize the inner dictionary
                    //    nodeData.Properties = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options);
                    //    break;

                    default:
                        // Handle unknown properties by skipping them
                        //reader.Skip();
                        break;
                }

            }

            throw new JsonException("Unexpected end of JSON.");
        }

        public override void Write(Utf8JsonWriter writer, DraggableNodeData value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            var properties = value.GetType().GetProperties();
            foreach( var property in properties )
            {
                var propertyValue = property.GetValue(value);
                if( propertyValue == null ) continue;

                writer.WritePropertyName(property.Name);

                // Manually handle nested collections
                if( property.Name == "Children" && propertyValue is List<DraggableNodeData> children )
                {
                    foreach( DraggableNodeData child in children )
                    {
                        //writer.WriteStartObject();
                        Write(writer, child, options);
                        //writer.WriteEndObject();
                    }
                }
                //else if( property.Name == "Subtasks" && propertyValue is List<IssueData.SubTask> subtasks )
                //{
                //    //Write(writer, subtasks.Cast<IssueData.Issue>().ToList(), options);
                //}
                //else if( property.Name == "LinkedIssues" && propertyValue is List<IssueData.Issue> linkedIssues )
                //{
                //    //Write(writer, linkedIssues, options);
                //}
                else
                {
                    // Use the default serializer for all other properties
                    JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
                }
            }

            writer.WriteEndObject();
        }
    }

    public class IssueConverter : JsonConverter<List<IssueData.Issue>>
    {
        public override List<IssueData.Issue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if( reader.TokenType != JsonTokenType.StartArray )
            {
                throw new JsonException("Expected start of array for Issues.");
            }

            var issues = new List<IssueData.Issue>();

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

        private IssueData.Issue ReadSingleIssue(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var rootElement = jsonDocument.RootElement;

            if( !rootElement.TryGetProperty("IssueType", out var issueTypeProperty) )
            {
                throw new JsonException("Missing 'IssueType' property.");
            }
            string issueType = issueTypeProperty.GetString()!;

            IssueData.Issue issue;
            switch( issueType )
            {
                //case "Sub-task":
                case JiraIssueType.Sub_task:
                    issue = new IssueData.SubTask();
                    break;
                case JiraIssueType.Test:
                    issue = new IssueData.TestTask();
                    break;
                case JiraIssueType.Task:
                    issue = new IssueData.TaskIssue();
                    break;
                case JiraIssueType.Story:
                case "Bug":
                case "Epic":
                    issue = new IssueData.Issue();
                    break;
                default:
                    issue = new IssueData.Issue();
                    break;
            }

            foreach( var property in rootElement.EnumerateObject() )
            {
                var propertyInfo = issue.GetType().GetProperty(property.Name);
                if( propertyInfo == null ) continue;

                if( ( property.Name == "Subtasks" || property.Name == "LinkedIssues" ) && property.Value.ValueKind == JsonValueKind.Array )
                {
                    List<IssueData.Issue>? nestedIssues = DeserializeNestedIssues(property.Value, options);

                    // Correctly cast the list based on the property name
                    if( property.Name == "Subtasks" )
                    {
                        try
                        {
                            var subtasksOnly = nestedIssues?.OfType<IssueData.SubTask>().ToList();
                            propertyInfo.SetValue(issue, subtasksOnly);

                            // Cast to List<SubTask>
                            //propertyInfo.SetValue(issue, nestedIssues?.Cast<IssueData.SubTask>().ToList());
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
                else if( property.Name == "Component" && issue is IssueData.TaskIssue taskIssue )
                {
                    var componentJson = property.Value.GetRawText();
                    taskIssue.Component = JsonSerializer.Deserialize<IssueData.Component>(componentJson, options);
                }
                else
                {
                    var value = JsonSerializer.Deserialize(property.Value.GetRawText(), propertyInfo.PropertyType, options);
                    propertyInfo.SetValue(issue, value);
                }
            }

            return issue;
        }

        private List<IssueData.Issue>? DeserializeNestedIssues(JsonElement element, JsonSerializerOptions options)
        {
            if( element.ValueKind != JsonValueKind.Array )
            {
                return null;
            }

            var nestedIssues = new List<IssueData.Issue>();
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

        public override void Write(Utf8JsonWriter writer, List<IssueData.Issue> value, JsonSerializerOptions options)
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
                    if( property.Name == "Subtasks" && propertyValue is List<IssueData.SubTask> subtasks )
                    {
                        Write(writer, subtasks.Cast<IssueData.Issue>().ToList(), options);
                    }
                    else if( property.Name == "LinkedIssues" && propertyValue is List<IssueData.Issue> linkedIssues )
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

        //public override void Write(Utf8JsonWriter writer, List<IssueData.Issue> value, JsonSerializerOptions options)
        //{
        //    writer.WriteStartArray();

        //    if( value != null )
        //    {
        //        foreach( var issue in value )
        //        {
        //            switch( issue.IssueType )
        //            {
        //                case "Task":
        //                    JsonSerializer.Serialize(writer, (IssueData.TaskIssue)issue, options);
        //                    break;
        //                case "Sub-task":
        //                    JsonSerializer.Serialize(writer, (IssueData.SubTask)issue, options);
        //                    break;
        //                case "Test":
        //                    JsonSerializer.Serialize(writer, (IssueData.TestTask)issue, options);
        //                    break;
        //                default:
        //                    JsonSerializer.Serialize(writer, issue, options);
        //                    break;
        //            }

        //            //JsonSerializer.Serialize(writer, issue, issue.GetType(), options);
        //            //
        //        }

        //    }
        //    writer.WriteEndArray();
        //}
    }
}
