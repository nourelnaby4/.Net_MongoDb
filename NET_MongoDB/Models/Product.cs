namespace NET_MongoDB.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        public string Name { get; set; }
        [BsonIgnoreIfNull] 
        public string CategoryId { get; set; }
        [BsonIgnoreIfNull]

        public string CategoryName { get; set; }
    }
}
