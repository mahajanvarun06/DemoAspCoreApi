namespace SampleCoreApi.Test
{
    public class Token
    {
        public string accessToken { get; set; }

        public string tokenType { get; set; }

        public int expiresIn { get; set; }

        public string refreshToken { get; set; }
    }
}
