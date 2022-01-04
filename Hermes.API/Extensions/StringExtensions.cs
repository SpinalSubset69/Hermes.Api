namespace Hermes.API.Extensions
{
    public static class StringExtensions
    {
        public static string Base64WithoutHeader(this string source)
        {
            string base64 = source.Split(",")[1];
            return base64;
        }
    }
}
