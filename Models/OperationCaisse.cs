namespace EasytransitCaisse.Models
{
    public class OperationCaisse
    {
        public int Id { get; set; }

        public int JourneeCaisseId { get; set; }

        public DateTime DateOperation { get; set; }

        public string TypeOperation { get; set; } // Encaissement / Décaissement

        public decimal Montant { get; set; }

        public string Libelle { get; set; }

        public int? ClientId { get; set; }

        public int CaisseId { get; set; }

        public int UtilisateurId { get; set; }

        public virtual JourneeCaisse JourneeCaisse { get; set; }

        public virtual Client Client { get; set; }

        public virtual Utilisateur Utilisateur { get; set; }
        public Caisse Caisse { get; set; } // 🔥 AJOUT IMPORTANT

    }
}