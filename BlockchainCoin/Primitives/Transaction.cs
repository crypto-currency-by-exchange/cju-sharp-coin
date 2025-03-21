using Newtonsoft.Json;

namespace ShareInvest.Primitives;

public class Transaction
{
    [JsonProperty(Order = 1)]
    public string? SendAddress
    {
        get; set;
    }

    [JsonProperty(Order = 2)]
    public string? ReceiveAddress
    {
        get; set;
    }

    [JsonProperty(Order = 3)]
    public double Amount
    {
        get; set;
    }

    [JsonProperty(Order = 4)]
    public long Timestamp
    {
        get; set;
    }
}