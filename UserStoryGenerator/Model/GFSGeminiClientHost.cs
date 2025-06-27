using GenerativeAI;

namespace UserStoryGenerator.Model
{
    public class GeminiRAGService
    {
        private readonly GenerativeModel _model;
        private static readonly char[] separator = [' ', ',', '.', '?', '!', '\'', '"'];

        //private readonly string _modelName; // Storing model name for error messages

        /// <summary>
        /// Initializes a new instance of the GeminiRAGService.
        /// </summary>
        /// <param name="apiKey">Your Google Gemini API Key. For production, consider
        /// using Google Cloud authentication (e.g., service accounts) for enhanced security.</param>
        /// <param name="modelName">The name of the Gemini model to use (e.g., "gemini-1.5-flash", "gemini-pro").
        /// "gemini-1.5-flash" is recommended for its large context window suitable for RAG.</param>
        public GeminiRAGService(string apiKey, string modelName = "gemini-1.5-flash")
        {
            if( string.IsNullOrWhiteSpace(apiKey) )
            {
                throw new ArgumentException("API Key cannot be null or empty.", nameof(apiKey));
            }
            if( string.IsNullOrWhiteSpace(modelName) )
            {
                throw new ArgumentException("Model name cannot be null or empty.", nameof(modelName));
            }

            // Corrected instantiation for the official Google.GenerativeAI package:
            // GenerativeModel is instantiated directly with the API key and model name.
            _model = new GenerativeModel(apiKey, modelName);
            //_modelName = modelName;
        }

        /// <summary>
        /// Executes a RAG (Retrieval-Augmented Generation) process.
        /// This simplified version performs keyword-based retrieval from provided documents.
        /// In a real-world RAG system, this would involve vector embeddings and a vector database.
        /// </summary>
        /// <param name="userQuery">The user's question or prompt.</param>
        /// <param name="knowledgeBaseDocuments">A list of strings representing the knowledge base documents
        /// from which relevant information should be retrieved.</param>
        /// <returns>The generated response from Gemini AI, augmented by the retrieved information.</returns>
        public async Task<string> AskGeminiWithRAG(string userQuery, List<string> knowledgeBaseDocuments)
        {
            Console.WriteLine($"\n--- RAG Process for Query: '{userQuery}' ---");

            // --- Step 1: Retrieval (Simplified Keyword-Based) ---
            // In a production RAG system, this would typically involve:
            // 1. Embedding the `userQuery` into a vector.
            // 2. Querying a specialized vector database (e.g., Pinecone, Weaviate, Milvus, Qdrant)
            //    containing pre-embedded chunks of your `knowledgeBaseDocuments`.
            // 3. Retrieving the top-N most semantically similar document chunks.
            // 4. Potentially re-ranking the retrieved documents for even greater relevance.

            Console.WriteLine("Performing simplified retrieval...");
            var relevantDocuments = new List<string>();
            var keywords = userQuery.ToLower()
                                     .Split(separator, StringSplitOptions.RemoveEmptyEntries)
                                     .Where(k => k.Length > 2) // Filter out very short keywords
                                     .Distinct()
                                     .ToList();

            // Simple keyword matching for demonstration
            foreach( var doc in knowledgeBaseDocuments )
            {
                var lowerDoc = doc.ToLower();
                // Check if any significant keyword from the query is present in the document
                if( keywords.Any(keyword => lowerDoc.Contains(keyword)) )
                {
                    relevantDocuments.Add(doc);
                }
            }

            string retrievedContent = string.Empty;
            if( relevantDocuments.Count != 0 )
            {
                // Concatenate relevant documents for context. Be mindful of token limits.
                // For very large documents, you might need to further chunk or summarize.
                retrievedContent = string.Join("\n\n---\n\n", relevantDocuments);
                //Console.WriteLine($"Retrieved {relevantDocuments.Count} relevant document(s).");
                //Console.WriteLine("Retrieved content snippet:\n" + ( retrievedContent.Length > 200 ? retrievedContent.Substring(0, 200) + "..." : retrievedContent ));
            }
            else
            {
                //Console.WriteLine("No relevant documents found for the query in the provided knowledge base.");
            }

            // --- Step 2: Augmentation ---
            // Combine the user's original query with the retrieved content to form an augmented prompt.
            // This provides the LLM with specific, up-to-date context, helping it generate a more
            // accurate, factual, and grounded response, reducing hallucinations.
            string augmentedPrompt;
            if( !string.IsNullOrEmpty(retrievedContent) )
            {
                augmentedPrompt = $"Based on the following contextual information:\n\n---\n{retrievedContent}\n---\n\nNow, answer the following question: {userQuery}";
            }
            else
            {
                // If no relevant content was retrieved, just use the original query.
                // The LLM will rely on its pre-trained knowledge.
                augmentedPrompt = userQuery;
            }

            //Console.WriteLine("\nSending augmented prompt to Gemini AI...");
            //Console.WriteLine("Augmented Prompt (excerpt):\n" + ( augmentedPrompt.Length > 500 ? augmentedPrompt.Substring(0, 500) + "..." : augmentedPrompt ));

            // --- Step 3: Generation ---
            try
            {
                // Directly use GenerateContentAsync on the _model instance.
                // This is the correct and definitively asynchronous method for the official Google.GenerativeAI package.
                var response = await _model.GenerateContentAsync(augmentedPrompt);

                // Extract and return the generated text from the model's response.
                return response.Text;
            }
            catch( Exception )
            {
                // Basic error handling. In a real application, you'd want more robust logging.
                //Console.Error.WriteLine($"\nError during Gemini AI generation (Model: {_modelName}): {ex.Message}");
                // Optionally, return a user-friendly error message or rethrow
                return "An error occurred while generating a response from the AI. Please try again.";
            }
        }
    }

    public class GFSGeminiClientHost
    {
        public delegate void LookupCompletedEventHandler(Result result);//string text
        public event LookupCompletedEventHandler? LookupCompleted;

        //private readonly GFSGeminiClient.IGFSGeminiClient? gfsGeminiClient;
        private readonly GFSGeminiClient.GFSGenerativeAIClient? gfsGeminiClient;

        //public int MaxOutputTokens { get; set; } = 4096;
        //public float Temperature { get; set; } = 0.2f; //0.2f, // Low for precision
        //public float TopP { get; set; } = 0.6f; // Moderate to low for focus
        //public float TopK { get; set; } = 30; // Moderate for variety within focus
        public int MaxOutputTokens
        {
            get { if( gfsGeminiClient == null ) return 4096; return this.gfsGeminiClient.MaxOutputTokens; }
            set { if( gfsGeminiClient == null ) return; this.gfsGeminiClient.MaxOutputTokens = value; }
        }

        public float Temperature
        {
            get { if( gfsGeminiClient == null ) return 0.2f; return this.gfsGeminiClient.Temperature; }
            set { if( gfsGeminiClient == null ) return; this.gfsGeminiClient.Temperature = value; }
        }
        public float TopP
        {
            get { if( gfsGeminiClient == null ) return 0.6f; return this.gfsGeminiClient.TopP; }
            set { if( gfsGeminiClient == null ) return; this.gfsGeminiClient.TopP = value; }
        }
        public float TopK
        {
            get { if( gfsGeminiClient == null ) return 30; return this.gfsGeminiClient.TopK; }
            set { if( gfsGeminiClient == null ) return; this.gfsGeminiClient.TopK = value; }
        }



        public string? query;
        public string? Query
        {
            get { return query; }
            internal set { query = value; }
        }

        public enum AIType
        {
            GenerativeAI,
            DotnetGeminiSDK
        }

        public GFSGeminiClientHost(string key, AIType aiType, string? geminiModel = null)
        {
            //if( aiType == AIType.DotnetGeminiSDK )
            //    gfsGeminiClient = new GFSGeminiClient.GFSDotnetGeminiSDKClient(key);
            //else
            {
                if( geminiModel == null )
                    geminiModel = Mscc.GenerativeAI.Model.Gemini20Flash001;//  "Gemini20Flash001";//Gemini25Flash  Gemini20FlashLite001
                else
                    geminiModel = Utilities.GeminiUtilities.GetGeminiModel(geminiModel);

                if( geminiModel != null )
                {
                    gfsGeminiClient = new GFSGeminiClient.GFSGenerativeAIClient(key, geminiModel)
                    {
                        MaxOutputTokens = 4096,
                        Temperature = 0.2f, //0.2f, // Low for precision
                        TopP = 0.6f, // Moderate to low for focus
                        TopK = 30, // Moderate for variety within focus
                    };
                }
                //
            }

            if( gfsGeminiClient != null )
            {
                gfsGeminiClient.Completed += (answer) =>
                {
                    LookupCompleted?.Invoke(new(id, answer));
                    this.id = null;
                };
            }
            //
        }

        /**/
        public async void EstimateTextTokens()
        {
            if( query == null ) return;
            if( gfsGeminiClient == null ) return;

            GFSGeminiClient.CountTokensResponse? countTokensResponse = await gfsGeminiClient.EstimateTextTokens(query);
            if( countTokensResponse != null )
            {
            }
        }

        object? id = null;
        internal async Task RequestAnswer(object? id = null)
        {
            if( query == null ) return;
            if( gfsGeminiClient == null ) return;

            this.id = id;

            await Task.Delay(0);
            _ = gfsGeminiClient.Request(query);
        }

        public class Result(object? id, string? answer)
        {
            public object? Id { get; } = id;
            public string? Answer { get; } = answer;
            public int ErrorCode { get; internal set; } = 0;
        }
    }

}


