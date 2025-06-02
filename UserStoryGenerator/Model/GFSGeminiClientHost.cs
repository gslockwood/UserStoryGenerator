using UserStoryGenerator.Utilities;

namespace UserStoryGenerator.Model
{
    public class GFSGeminiClientHost
    {
        public delegate void LookupCompletedEventHandler();//string text
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

        internal async Task RequestAnswer()
        {
            if( query == null ) return;

            try
            {
                string? answer = await gfsGeminiClient.Request(query);
                if( answer != null )
                {
                    //Logger.Info(answer);

                    Answers?.Add(answer);
                    LookupCompleted?.Invoke();//answer
                }
                else
                {
                    //throw new NullReferenceException(nameof(answer));
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
    }

}


