﻿using Newtonsoft.Json;

namespace Zabbix.Entities;

public class HostGroup : BaseEntity
{
    #region Properties

    [JsonProperty("groupid")]
    public override string? EntityId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("internal")]
    public int? Internal { get; set; }

    [JsonProperty("flags")]
    public int? Flags { get; set; }

    #endregion

    #region Components

    [JsonProperty("discoveryRule")]
    public IList<DiscoveryRule>? DiscoveryRules { get; set; }
    [JsonProperty("groupDiscovery")]
    public IList<GroupDiscovery>? GroupDiscoveries { get; set; }
    [JsonProperty("hosts")]
    public IList<Host>? Hosts { get; set; }
    [JsonProperty("templates")]
    public IList<Template>? Templates { get; set; }

    #endregion

    #region Constructors

    public HostGroup(string name) => Name = name;

    public HostGroup()
    {
    }

    #endregion
}

public class GroupDiscovery
{
    #region Properties

    [JsonProperty("groupid")]
    public string? GroupId { get; set; }

    [JsonProperty("lastcheck")]
    public string? LastCheck { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("parent_group_prototypeid")]
    public string? ParentGroupPrototypeId { get; set; }

    [JsonProperty("ts_delete")]
    public string? TsDelete { get; set; }

    #endregion

}


/// <summary>
/// Simplified host group reference for use when only the group ID is needed
/// </summary>
public class HostGroupReference
{
    [JsonProperty("groupid")]
    public string? GroupId { get; set; }

    public HostGroupReference(string? groupId)
    {
        GroupId = groupId;
    }

    public HostGroupReference()
    {
    }
}

