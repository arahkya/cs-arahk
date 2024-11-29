using System.Security.Cryptography;

namespace Arahk.Domain.Identity.Services;

public static class PasswordHashService
{
    private const int SaltSize = 16; // 128-bit
    private const int HashSize = 32; // 256-bit
    private const int Iterations = 100000; // Number of PBKDF2 iterations
    
    /// <summary>
    /// Hashes a password with a randomly generated salt.
    /// </summary>
    /// <param name="password">The plain text password.</param>
    /// <returns>A hash string containing the salt and hashed password.</returns>
    public static string HashPassword(string password)
    {
        // Generate a random salt
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        // Hash the password with the salt
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(HashSize);

        // Combine the salt and hash into a single string
        var hashBytes = new byte[SaltSize + HashSize];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
        Buffer.BlockCopy(hash, 0, hashBytes, SaltSize, HashSize);

        // Return as a Base64-encoded string
        return Convert.ToBase64String(hashBytes);
    }
    
    /// <summary>
    /// Verifies a password against a hashed password.
    /// </summary>
    /// <param name="password">The plain text password.</param>
    /// <param name="hashedPassword">The hashed password (salt + hash).</param>
    /// <returns>True if the password matches the hash; otherwise, false.</returns>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Decode the Base64-encoded hash
        var hashBytes = Convert.FromBase64String(hashedPassword);

        // Extract the salt from the hash
        var salt = new byte[SaltSize];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);

        // Extract the hash from the hashBytes
        var originalHash = new byte[HashSize];
        Buffer.BlockCopy(hashBytes, SaltSize, originalHash, 0, HashSize);

        // Hash the provided password with the same salt
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var newHash = pbkdf2.GetBytes(HashSize);

        // Compare the hashes securely
        return CryptographicOperations.FixedTimeEquals(originalHash, newHash);
    }
}