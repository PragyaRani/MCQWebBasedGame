using System;
using System.Security.Cryptography;

namespace MCQPuzzleGame.Context
{
    public static class PasswordHash
    {
        const int SaltSize = 16, HashSize = 20, HashIteration = 10000;
        // Create Hash from password
        static byte[] _salt, _hash;
        static string Hash(string password, int iteration)
        {
            // create salt
             new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
            // create hash
            _hash = new Rfc2898DeriveBytes(password, _salt, iteration).GetBytes(HashSize);
            var hashbytes = new byte[SaltSize + HashSize];
            // combine salt and hash
            Array.Copy(_salt, 0, hashbytes, 0, SaltSize);
            Array.Copy(_hash, 0, hashbytes, SaltSize, HashSize);
            // convert to base 64
            return Convert.ToBase64String(hashbytes);
        }
        public static string Hash(string password)
        {
            return Hash(password, HashIteration);
        }
        public static bool IsVerify(string password, string hashedPassword)
        {
            byte[] hashbPassword = Convert.FromBase64String(hashedPassword);
            Array.Copy(hashbPassword, 0, _salt = new byte[SaltSize],0, SaltSize);
            Array.Copy(hashbPassword, SaltSize, _hash = new byte[HashSize], 0, HashSize);
            byte[] test = new Rfc2898DeriveBytes(password,_salt,HashIteration).GetBytes(HashSize);
            for(int i=0; i<HashSize; i++)
            {
                if(test[i] != _hash[i])
                {
                    return false;
                   
                }
            }
            return true;
        }
    }
}
