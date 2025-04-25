﻿using Newtonsoft.Json;

namespace Zabbix.Entities;

public class HighAvailabilityNode : BaseEntity
{
    #region Properties

    [JsonProperty("ha_nodeid")]
    public override string? EntityId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("address")]
    public string? Address { get; set; }

    [JsonProperty("port")]
    public int? Port { get; set; }

    [JsonProperty("lastaccess")]
    public int? LastAccess { get; set; }

    [JsonProperty("status")]
    public int? Status { get; set; }

    #endregion

}
