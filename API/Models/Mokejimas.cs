using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace API.Models
{
	public class Mokejimas
	{

		public Guid Id { get; set; }

		public Guid Vartotojo_id { get; set; }
		public double Suma { get; set; }

		public List<Guid> PirktosPrekes { get; set; }
	}
}