namespace BP.Application
{
    public class JwtOptions
    {
        public string ISSUER { get; set; }
        public string AUDIENCE { get; set; }
        public string KEY { get; set; }
        public int EXPIRESHOURS { get; set; }
    }
}
