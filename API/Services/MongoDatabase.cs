using MongoDB.Driver;

namespace API.Services
{
    public class MongoDatabase
    {
        public IMongoDatabase Database { get; }
        public MongoDatabase()
        {
            var client = new MongoClient("mongodb+srv://manopazymiai1:QRHBJzqeXhD6qMp8@cluster0.pfnuc8v.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
            this.Database = client.GetDatabase("meno");
        }
    }
}
