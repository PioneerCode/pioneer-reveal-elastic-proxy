namespace Pioneer.Reveal.ElasticProxy.Api.Repository
{
    public static class ProxyRoutes
    {
        public const string GetIndices = "_cat/indices";
        public const string GetLogs = "{indices}/_search";
    }
}
