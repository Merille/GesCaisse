using EasytransitCaisse.Models;
using System.Security.Cryptography;
using System.Text;

namespace EasytransitCaisse.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Utilisateurs.Any())
            {
                var admin = new Utilisateur
                {
                    NomUtilisateur = "admin",
                    NomComplet = "Administrateur",
                    Profil = "Admin",
                    MotPasse = HashPassword("123")
                };

                context.Utilisateurs.Add(admin);


                context.SaveChanges();
            }

            if (!context.Caisses.Any())
            {
                var caisse = new Caisse
                {
                    ChkCode = "CASH1",
                    ChkDescription = "Caisse principale",
                    StCode = "ST01",
                    SalesPersonDefault = 1,
                    CashierDefault = 1,
                    CustomerDefaultCode = "CUST001",
                    JournalDefaultCode = "JRN01"
                };
                context.Caisses.Add(caisse);
                context.SaveChanges();
            }

        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}