using API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace API.Services
{
	public class KrepselisService
	{
		private IMongoCollection<Krepselis> _krepselisCollection;
		private IMongoCollection<Mokejimas> _mokejimasCollection;
		private readonly IMongoCollection<Preke> _prekeCollection;

		public KrepselisService(MongoDatabase database)
		{
			_krepselisCollection = database.Database.GetCollection<Krepselis>("Krepselis");
			_prekeCollection = database.Database.GetCollection<Preke>("Preke");
			_mokejimasCollection = database.Database.GetCollection<Mokejimas>("Mokejimas");
		}
		public List<Mokejimas> GetMokejimai(Guid clientId)
		{
			var mokejimai = _mokejimasCollection.Find(m => m.Vartotojo_id == clientId).ToList();
			return mokejimai;
		}

		public Mokejimas InsertMokejimas(List<PrekiuKrepselis> items)
		{
			var mokejimas = new Mokejimas
			{
				Vartotojo_id = items?.FirstOrDefault()?.Vartotojo_id ?? Guid.Empty, 
				Suma = 0,
				PirktosPrekes = new List<Guid>()
			};

			foreach (var item in items)
			{
				var preke = _prekeCollection.Find(p => p.Id == item.Preke_id).FirstOrDefault();

				if (preke != null)
				{
					mokejimas.Suma += preke.Kaina * item.Kiekis;
					mokejimas.PirktosPrekes.Add(item.Preke_id);
				}
			}
			mokejimas.Id = Guid.NewGuid();

			_mokejimasCollection.InsertOne(mokejimas);

			return mokejimas;
		}

public void AddKrepselis(Krepselis item)
		{
			_krepselisCollection.InsertOne(item);
		}

		public List<PrekiuKrepselis> GetPrekiuKrepseliai(Guid userId)
		{
			var prekiuKrepseliai = _krepselisCollection
				.Find(item => item.Vartotojo_id == userId)
				.ToList();

			var result = new List<PrekiuKrepselis>();

			foreach (var krepselis in prekiuKrepseliai)
			{
				var preke = _prekeCollection.Find(p => p.Id == krepselis.Preke_id).FirstOrDefault();

				if (preke != null)
				{
					var prekiuKrepselis = new PrekiuKrepselis
					{
						Id = krepselis.Id,
						Preke_id = krepselis.Preke_id,
						Vartotojo_id = krepselis.Vartotojo_id,
						Kiekis = krepselis.Kiekis,
						Pavadinimas = preke.Pavadinimas,
						Kaina = preke.Kaina
					};

					result.Add(prekiuKrepselis);
				}
			}

			return result;
		}

		public void DeleteKrepselis(Guid id)
		{
			_krepselisCollection.DeleteOne(item => item.Id == id);
		}
		public void DeleteAllKrepselis(Guid id)
		{
			_krepselisCollection.DeleteMany(item => item.Vartotojo_id == id);
		}

		public void UpdateKrepselis(Guid id, Krepselis item)
		{
			var updateDef = Builders<Krepselis>.Update
				.Set(i => i.Kiekis, item.Kiekis);

			_krepselisCollection.UpdateOne(item => item.Id == id, updateDef);
		}
	}
}
