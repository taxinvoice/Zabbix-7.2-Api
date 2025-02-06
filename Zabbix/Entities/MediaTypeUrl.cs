using Newtonsoft.Json;

namespace Zabbix.Entities;

public class MediaTypeUrl
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }
}