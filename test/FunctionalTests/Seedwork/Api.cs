namespace FunctionalTests.Seedwork
{
    public static class Api
    {
        public static class V1
        {
            private const string VERSION = "?version=1.0";

            public static class Notes
            {
                public static string Get => $"api/notes{VERSION}";
                public static string GetBy(int id) => $"api/notes/{id}{VERSION}";
                public static string Post => $"api/notes{VERSION}";
                public static string Patch(int id) => $"api/notes/{id}{VERSION}";
                public static string Delete(int id) => $"api/notes/{id}{VERSION}";
            }
        }
    }
}
