using EasytransitCaisse.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using EasytransitCaisse.Models;

namespace EasytransitCaisse.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        // HASH PASSWORD
        public string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        // LOGIN
        public async Task<Utilisateur?> Login(string username, string password)
        {
            var hash = HashPassword(password);

            return await _context.Utilisateurs
                .FirstOrDefaultAsync(u =>
                    u.NomUtilisateur == username &&
                    u.MotPasse == hash);
        }

        // REGISTER
        public async Task<Utilisateur> Register(Utilisateur user)
        {
            user.MotPasse = HashPassword(user.MotPasse);

            _context.Utilisateurs.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}