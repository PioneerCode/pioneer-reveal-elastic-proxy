namespace Pioneer.Reveal.ElasticProxy.Api.Entites
{
    public class PioneerRevealConfiguraiton
    {
        /// <summary>
        /// Secret key used to encode JWT
        /// </summary>
        public string JwtSecret { get; set; }

        /// <summary>
        /// Username to pass authentication
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Associated password used to pass authentication
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Elastic Search URL
        /// </summary>
        public string ElasticUrl { get; set; }
    }
}
