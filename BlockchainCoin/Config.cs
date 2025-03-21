using RocksDbSharp;

namespace ShareInvest;

static class Config
{
    internal static DbOptions Options
    {
        get => options;
    }

    internal static ColumnFamilies ColumnFamilies
    {
        get => columnFamilies;
    }

    internal static int MiningDifficulty
    {
        get; private set;
    }
        = MINING_DIFFICULTY;

    static readonly ColumnFamilies columnFamilies = new()
    {
        { "block", new ColumnFamilyOptions() },
        { "utxo", new ColumnFamilyOptions() },
        { "txpool", new ColumnFamilyOptions() },
    };

    static readonly DbOptions options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();

    const int MINING_DIFFICULTY = 5;
}