using RocksDbSharp;

using ShareInvest.Primitives;

namespace ShareInvest;

public class Chain
{
    public bool CreateGenesisBlock(string genesisOutputScript, uint nonce = 0, uint genesisReward = 1)
    {
        using (var db = RocksDb.Open(Config.Options, Environment.CurrentDirectory, Config.ColumnFamilies))
        {
            var cf = db.GetColumnFamily("block");

            using (var iterator = db.NewIterator(cf: cf))
            {
                iterator.Seek("block_");

                if (iterator.Valid())
                {
                    Console.WriteLine("Block aleady exists.");

                    return false;
                }
            }

            var block = new Block
            {
                PrevHash = new string('0', 0x40),
                Nonce = nonce,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Transactions = [
                    new() {
                        SendAddress = string.Empty,
                        ReceiveAddress = genesisOutputScript,
                        Amount = genesisReward,
                        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    }
                ]
            };
            db.Put("block_0", block.Serialize(), cf: cf);

            return true;
        }
    }

    public void CreateBlock(string prevHash, uint nonce, IEnumerable<Transaction> transactions)
    {
        using (var db = RocksDb.Open(Config.Options, Environment.CurrentDirectory, Config.ColumnFamilies))
        {
            var cf = db.GetColumnFamily("block");

            using (var iterator = db.NewIterator(cf: cf))
            {
                iterator.SeekToLast();

                if (iterator.Valid())
                {
                    var block = new Block
                    {
                        PrevHash = prevHash,
                        Nonce = nonce,
                        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                        Transactions = transactions
                    };

                    if (int.TryParse(iterator.StringKey().Split('_')[^1], out int index))
                    {
                        db.Put($"block_{1 + index}", block.Serialize(), cf: cf);
                    }
                }
            }
        }
    }

    public bool ValidChain(Block[] chain)
    {
        for (int index = 1; index < chain.Length; index++)
        {
            if (Hash.ComputeHash(chain[index - 1]) is string hash)
            {
                if (hash.Equals(chain[index].PrevHash))
                {
                    continue;
                }

                if (Hash.CheckBlock(hash))
                {
                    continue;
                }
            }
            return false;
        }
        return true;
    }
}