using UserStoryGenerator.Utilities;

namespace UserStoryGenerator.Model
{
    public class GFSGeminiClientHost
    {
        public delegate void LookupCompletedEventHandler(Result result);//string text
        public event LookupCompletedEventHandler? LookupCompleted;

        private readonly GFSGeminiClient.IGFSGeminiClient gfsGeminiClient;

        public IList<string>? Answers { get; private set; }

        public string? query;
        public string? Query
        {
            get { return query; }
            internal set
            {
                query = value;
                Answers = [];
            }
        }
        //public string? Answer { get; internal set; }

        public enum AIType
        {
            GenerativeAI,
            DotnetGeminiSDK
        }

        public GFSGeminiClientHost(string key, AIType aiType)
        {
            if( aiType == AIType.DotnetGeminiSDK )
                gfsGeminiClient = new GFSGeminiClient.GFSDotnetGeminiSDKClient(key);

            else
                gfsGeminiClient = new GFSGeminiClient.GFSGenerativeAIClient(key);

            Answers = [];
            //
        }

        internal async Task RequestAnswer(object? id = null)
        {
            if( query == null ) return;

            try
            {
                string? answer = await gfsGeminiClient.Request(query);
                if( answer != null )
                {
                    //Logger.Info(answer);

                    Answers?.Add(answer);

                    //string answer = answers.First();

                    Result result = new(id, answer);
                    LookupCompleted?.Invoke(result);//answer
                }
                else
                {
                    //throw new NullReferenceException(query);
                    LookupCompleted?.Invoke(new(id, null));
                }

            }
            catch( Exception ex )
            {
                Logger.Info(ex.Message);
                if( ex.StackTrace != null )
                    Logger.Info(ex.StackTrace);
                throw;
            }
        }

        public class Result(object? id, string? answer)
        {
            public object? Id { get; } = id;
            public string? Answer { get; } = answer;
            //public List<IssueData.Issue>? Issues { get; internal set; }

        }
    }

}


