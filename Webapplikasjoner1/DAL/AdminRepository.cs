using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public class AdminRepository : IAdminRepository {

        private readonly BillettKontekst _db;
        
        private ILogger<AdminRepository> _logger;

        public AdminRepository(BillettKontekst db, ILogger<AdminRepository> logger) 
        {
            _db = db;
            _logger = logger;
        }

  
    
        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: passord,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512, // bruker SHA512 algoritmen
                iterationCount: 1000, // hashes 1000 ganger
                numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(Admin admin)
        {
            try
            {
                Adminer fAdmin = await _db.Admins.FirstOrDefaultAsync(a => a.Brukernavn == admin.Brukernavn);
                byte[] hash = LagHash(admin.Passord, fAdmin.Salt);
                bool ok = hash.SequenceEqual(fAdmin.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<bool> OpprettAdmin(Admin admin)
        {
            byte[] salt = LagSalt();
            byte[] hash = LagHash(admin.Passord, salt);

            Adminer nyAdmin = new Adminer
            {
                Brukernavn = admin.Brukernavn,
                Passord = hash,
                Salt = salt,
            };
            try
            {
                _db.Admins.Add(nyAdmin);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                return false;
            }
        }

        
    }
       }
