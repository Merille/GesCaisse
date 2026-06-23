namespace EasytransitCaisse.Models
{
    public class LigneFacture
    {
        public int Id { get; set; }

        public int FactureId { get; set; }

        public string Designation { get; set; }

        public decimal Quantite { get; set; }

        public decimal PrixUnitaire { get; set; }

        public decimal TotalLigne { get; set; }

        public virtual Facture Facture { get; set; }
    }
}