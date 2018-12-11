namespace Pioneer.Reveal.ElasticProxy
{
    public static class ProxyRoutes
    {
        public const string GetIndices = "_cat/indices";
        public const string GetLogs = "{indices}/_search";
    }
}
