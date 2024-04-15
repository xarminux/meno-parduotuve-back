using API.Models;
using MongoDB.Driver;

namespace API.Services
{
    public class ItemsService
    {
        private IMongoCollection<Preke> _collection;

        public ItemsService(MongoDatabase database)
        {
            this._collection = database.Database.GetCollection<Preke>("Preke");
        }

        public void AddItem(Preke item)
        {
            this._collection.InsertOne(item);
        }

        public Preke GetItem(Guid id)
        {
            var item = this._collection.Find(item => item.Id == id).First();
            return item;
        }

        public IEnumerable<Preke> GetItems()
        {
            return this._collection.Find(Builders<Preke>.Filter.Empty).ToEnumerable();
        }

        public void DeleteItem(Guid id)
        {
            this._collection.DeleteOne(item => item.Id == id);
        }

        public void UpdateItem(Guid id, Preke item)
        {
            var updateDef = Builders<Preke>.Update
                .Set(i => i.Pavadinimas, item.Pavadinimas)
                .Set(i => i.Aprasymas, item.Aprasymas)
                .Set(i => i.Kaina, item.Kaina)
                .Set(i => i.Paveikslelis, item.Paveikslelis);

            _collection.UpdateOne(item => item.Id == id, updateDef);
        }
    }
}
