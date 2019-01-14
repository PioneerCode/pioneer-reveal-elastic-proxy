namespace Pioneer.Reveal.ElasticProxy.Api.Entites
{
    public class PioneerRevealConfiguraiton
    {
        public string JwtSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SiteUrl { get; set; }
        public string ElasticUrl { get; set; }
        public string CorsOrigin { get; set; }
    }
}
