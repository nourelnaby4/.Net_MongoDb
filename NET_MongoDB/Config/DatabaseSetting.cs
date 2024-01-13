namespace NET_MongoDB.Config
{
    public class DatabaseSetting
    {
        public string? ConnectionString { get; set; }   
        public string? DatabaseName { get; set;}
        public string? CategoryCollectionName { get; set;}
        public string? ProductCollectionName { get; set;}
    }
}
