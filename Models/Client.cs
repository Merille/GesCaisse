namespace EasytransitCaisse.Models
{
    public class Client
    {
        public int Id { get; set; }

        // Société
        public string CodeClient { get; set; }
        public string NomSociete { get; set; }
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }

        // Interlocuteur principal
        public string? NomContact { get; set; }
        public string? FonctionContact { get; set; }
        public string? TelephoneContact { get; set; }
        public string? EmailContact { get; set; }
    }
}