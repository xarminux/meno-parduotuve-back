using API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace API.Services
{
    public class KrepselisService
    {
        private IMongoCollection<Krepselis> _krepselisCollection;
        private IMongoCollection<Kopija> _kopijaCollection;
        private readonly IMongoCollection<Preke> _prekeCollection;

        public KrepselisService(MongoDatabase database)
        {
            _krepselisCollection = database.Database.GetCollection<Krepselis>("Krepselis");
            _prekeCollection = database.Database.GetCollection<Preke>("Preke");
            _kopijaCollection = database.Database.GetCollection<Kopija>("Kopija");
        }
        public void AddKrepselis(Kopija item, Guid vartotojo_id)
        {
            Console.WriteLine(vartotojo_id);
            var existingKrepselis = _krepselisCollection.Find(k => k.Vartotojo_id == vartotojo_id).FirstOrDefault();

            if (existingKrepselis != null)
            {
                // Atnaujinti esamo krepšelio kainą
                var preke = _prekeCollection.Find(p => p.Id == item.Preke_Id).FirstOrDefault();
                if (preke != null)
                {
                    existingKrepselis.Suma += preke.Kaina;
                    _krepselisCollection.ReplaceOne(k => k.Id == existingKrepselis.Id, existingKrepselis);
                }
                item.Krepselis_Id = existingKrepselis.Id;
            }
            else
            {
                // Sukurti naują krepšelį ir pridėti kainą
                var krepselis = new Krepselis
                {
                    Id = Guid.NewGuid(),
                    Vartotojo_id = vartotojo_id,
                    Suma = 0
                };
                item.Krepselis_Id = krepselis.Id;
                var preke = _prekeCollection.Find(p => p.Id == item.Preke_Id).FirstOrDefault();
                if (preke != null)
                {
                    krepselis.Suma = preke.Kaina;
                }
                _krepselisCollection.InsertOne(krepselis);
            }

            _kopijaCollection.InsertOne(item);
        }
        public void RemovePrekeFromKrepselis(Guid kopijosId)
        {
            var kopija = _kopijaCollection.Find(k => k.Id == kopijosId).FirstOrDefault();

            if (kopija != null)
            {
                var krepselis = _krepselisCollection.Find(k => k.Id == kopija.Krepselis_Id).FirstOrDefault();
                if (krepselis != null)
                {
                    var preke = _prekeCollection.Find(p => p.Id == kopija.Preke_Id).FirstOrDefault();
                    if (preke != null)
                    {
                        krepselis.Suma -= preke.Kaina;
                        _krepselisCollection.ReplaceOne(k => k.Id == krepselis.Id, krepselis);
                    }
                }

                _kopijaCollection.DeleteOne(k => k.Id == kopija.Id);
            }

        }
        public List<Kopija> GetKopijosByVartotojoId(Guid vartotojoId)
        {
            var krepselioId = _krepselisCollection.Find(k => k.Vartotojo_id == vartotojoId).FirstOrDefault()?.Id;

            if (krepselioId != null)
            {
                var kopijos = _kopijaCollection.Find(k => k.Krepselis_Id == krepselioId).ToList();
                return kopijos;
            }

            return new List<Kopija>();
        }


    }


}
