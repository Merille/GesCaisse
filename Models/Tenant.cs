namespace EasytransitCaisse.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Code { get; set; }

        public bool Actif { get; set; } = true;

        public DateTime DateCreation { get; set; } = DateTime.Now;
    }
}
