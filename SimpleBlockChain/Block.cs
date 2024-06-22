using System.Security.Cryptography;
using System.Text;

namespace SimpleBlockChain;

/// <summary>The Block class represents a single block in the blockchain.</summary>
public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public string Data { get; set; }
    public int Nonce { get; set; }

    public Block(int index, DateTime timestamp, string previousHash, string data)
    {
        Index = index;
        Timestamp = timestamp;
        PreviousHash = previousHash;
        Data = data;
        Nonce = 0;
        Hash = index == 0 ? CalculateHash() : string.Empty;
    }

    /// <summary>Generates a SHA256 hash based on the block’s properties.</summary>
    public string CalculateHash()
    {
        using var sha256 = SHA256.Create();
        var rawData = $"{Index}{Timestamp}{PreviousHash}{Data}{Nonce}";
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
    
    /// <summary>
    /// Implements a simple proof-of-work algorithm by iterating
    /// the nonce until a hash with the required difficulty is found.
    /// </summary>

    public void MineBlock(int difficulty)
    {
        var hashValidation = new string('0', difficulty);
        while (Hash.Length == 0 || Hash.Substring(0, difficulty) != hashValidation)
        {
            Nonce++;
            Hash = CalculateHash();
        }
        Console.WriteLine($"Block mined: {Hash}");
        Console.WriteLine();
    }
}