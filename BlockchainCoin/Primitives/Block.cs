using Newtonsoft.Json;

namespace ShareInvest.Primitives;

public class Block
{
    [JsonProperty(Order = 1)]
    public string? PrevHash
    {
        get; set;
    }

    [JsonProperty(Order = 2)]
    public uint Nonce
    {
        get; set;
    }

    [JsonProperty(Order = 3)]
    public long Timestamp
    {
        get; set;
    }

    [JsonProperty(Order = 4)]
    public IEnumerable<Transaction>? Transactions
    {
        get; set;
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}