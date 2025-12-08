namespace FrontEASE.Client.Infrastructure.Utils
{
    public static class TextHelper
    {
        public static string TruncateString(string token, int length)
        {
            if(token.Length < length)
            {
                return token;
            }

            var parts = new List<string>()
            {
                new([.. token.Take((int)Math.Ceiling(length / 2.0))]),
                "...",
                new([.. token.TakeLast((int)Math.Ceiling(length / 2.0))]),
            };

            return string.Join(" ", parts);
        }
    }
}
