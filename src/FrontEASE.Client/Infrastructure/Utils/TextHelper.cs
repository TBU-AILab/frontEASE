namespace FrontEASE.Client.Infrastructure.Utils
{
    public static class TextHelper
    {
        public static string TruncateTokenString(string token)
        {
            if(token.Length < 6)
            {
                return token;
            }

            var parts = new List<string>()
            {
                new([.. token.Take(3)]),
                "...",
                new([.. token.TakeLast(3)]),
            };

            return string.Join(" ", parts);
        }
    }
}
