namespace UserStoryGenerator.Model
{
#pragma warning disable IDE1006 // Naming Styles
    public class BadRequestError
    {
        public Error? error { get; set; }


        public class Error
        {
            public int code { get; set; }
            public string? message { get; set; }
            public string? status { get; set; }
            public Detail[]? details { get; set; }
        }

        public class Detail
        {
            public string? type { get; set; }
            public string? reason { get; set; }
            public string? domain { get; set; }
            public Metadata? metadata { get; set; }
            public string? locale { get; set; }
            public string? message { get; set; }
        }

        public class Metadata
        {
            public string? service { get; set; }
        }
    }
#pragma warning restore IDE1006 // Naming Styles

}