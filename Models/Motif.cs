using EasytransitCaisse.Data;

namespace EasytransitCaisse.Models
{
    public class Motif : ITenantScoped
    {
        public int ID { get; set; } // Identity

        public string? LibelleMotif { get; set; }
        public string? TypeModif { get; set; }

        public string? CG_Num { get; set; }

        public int TenantId { get; set; }

    }
}
