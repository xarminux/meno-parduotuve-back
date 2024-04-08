

namespace API.Models
{
    public class Preke
    {
        public Guid Id { get; set; }
        public Guid fk_Valdytojas_Id { get; set; }
        public double Kaina { get; set; }
        public string Pavadinimas { get; set; }
        public string Aprasymas { get; set; }
        public string Paveikslelis { get; set; }
    }
}
