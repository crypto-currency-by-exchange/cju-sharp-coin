using ShareInvest.Primitives;

using System.Security.Cryptography;
using System.Text;

namespace ShareInvest;

public static class Hash
{
    public static string ComputeHash(Block block)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(block.Serialize()));

        var sb = new StringBuilder(hash.Length * 2);

        foreach (var b in hash)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    public static bool CheckBlock(string hash)
    {
        return hash[..Config.MiningDifficulty] == new string('0', Config.MiningDifficulty);
    }

    public static bool CheckBlock(Block block)
    {
        return ComputeHash(block)[..Config.MiningDifficulty] == new string('0', Config.MiningDifficulty);
    }
}