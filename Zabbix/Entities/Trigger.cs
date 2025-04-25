﻿using Newtonsoft.Json;

namespace Zabbix.Entities;

public class TriggerFunction
{
    #region Properties

    [JsonProperty("functionid")]
    public string? Functionid { get; set; }

    [JsonProperty("itemid")]
    public string? Itemid { get; set; }

    [JsonProperty("triggerid")]
    public string? Triggerid { get; set; }

    [JsonProperty("parameter")]
    public string? Parameter { get; set; }

    [JsonProperty("function")]
    public string? Function { get; set; }

    #endregion
}

public class Trigger : BaseEntity
{
    #region Properties

    [JsonProperty("triggerid")]
    public override string? EntityId { get; set; }

    [JsonProperty("expression")]
    public string? Expression { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("value")]
    public int? Value { get; set; }

    [JsonProperty("priority")]
    public int? Priority { get; set; }

    [JsonProperty("lastchange")]
    public string? Lastchange { get; set; }

    [JsonProperty("comments")]
    public string? Comments { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }

    [JsonProperty("templateid")]
    public string? Templateid { get; set; }

    [JsonProperty("type")]
    public int? Type { get; set; }

    [JsonProperty("state")]
    public int? State { get; set; }

    [JsonProperty("flags")]
    public int? Flags { get; set; }

    [JsonProperty("recovery_mode")]
    public int? RecoveryMode { get; set; }

    [JsonProperty("recovery_expression")]
    public string? RecoveryExpression { get; set; }

    [JsonProperty("correlation_mode")]
    public int? CorrelationMode { get; set; }

    [JsonProperty("correlation_tag")]
    public string? CorrelationTag { get; set; }

    [JsonProperty("manual_close")]
    public int? ManualClose { get; set; }

    [JsonProperty("opdata")]
    public string? Opdata { get; set; }

    #endregion

    #region Components
    [JsonProperty("functions")]
    public IList<Trigger>? TriggerFunctions { get; set; }
    [JsonProperty("tags")]
    public IList<Tag>? Tags { get; set; }
    [JsonProperty("items")]
    public IList<Item>? Items { get; set; }
    [JsonProperty("groups")]
    public IList<HostGroup>? HostGroups { get; set; }
    [JsonProperty("hosts")]
    public IList<Host>? Hosts { get; set; }
    [JsonProperty("lldrule")]
    public IList<DiscoveryRule>? DiscoveryRules { get; set; }

    #endregion

    #region Constructors

    public Trigger(string description, string expression)
    {
        Description = description;
        Expression = expression;
    }

    public Trigger()
    {
    }

    #endregion
}