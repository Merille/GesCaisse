using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasytransitCaisse.Models.ViewModels
{
    public class DashboardViewModel
    {
        // Caisse
        public decimal SoldeCaisse { get; set; }

        public decimal EntreesJour { get; set; }

        public decimal SortiesJour { get; set; }

        public decimal SoldeJour { get; set; }

        // Statistiques

        public int NombreOperations { get; set; }

        public int NombreEncaissements { get; set; }

        public int NombreDecaissements { get; set; }

        public int NombreCaissesOuvertes { get; set; }

        public int NombreUtilisateurs { get; set; }

        // Totaux

        public decimal TotalEncaissementsMois { get; set; }

        public decimal TotalDecaissementsMois { get; set; }

        public DateTime? DateDebut { get; set; }

        public DateTime? DateFin { get; set; }
        public string? StatutJournee { get; set; }

        public int? CaisseId { get; set; }

        public List<SelectListItem> ListeCaisses { get; set; } = new();


        // Dernières opérations

        public List<OperationDashboardViewModel> DernieresOperations { get; set; } = new();
    }

    public class OperationDashboardViewModel
    {
        public DateTime DateOperation { get; set; }

        public string Reference { get; set; }

        public string Libelle { get; set; }

        public string TypeOperation { get; set; }

        public decimal Montant { get; set; }

        public string Caissier { get; set; }
    }
}