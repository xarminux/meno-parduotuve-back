

namespace API.Models
{
	public class PrekiuKrepselis
	{
		public Guid Id { get; set; }
		public Guid Preke_id { get; set; }
		public Guid Vartotojo_id { get; set; }
		public int Kiekis { get; set; }
		public string Pavadinimas { get; set; }
		public double Kaina { get; set; }

	}
}