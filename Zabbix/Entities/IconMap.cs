﻿using Newtonsoft.Json;

namespace Zabbix.Entities;

public class IconMapping
{
    #region Properties

    [JsonProperty("iconmappingid")]
    public string? IconMappingId { get; set; }

    [JsonProperty("iconid")]
    public string? IconId { get; set; }

    [JsonProperty("expression")]
    public string? Expression { get; set; }

    [JsonProperty("inventory_link")]
    public int? InventoryLink { get; set; }

    [JsonProperty("iconmapid")]
    public string? IconMapId { get; set; }

    [JsonProperty("sortorder")]
    public int? SortOrder { get; set; }

    #endregion

    #region Constructors

    public IconMapping(string iconid, string expression, int inventoryLink)
    {
        IconId = iconid;
        Expression = expression;
        InventoryLink = inventoryLink;
    }

    public IconMapping()
    {
    }
    #endregion

}

public class IconMap : BaseEntity
{
    #region Properties

    [JsonProperty("iconmapid")]
    public override string? EntityId { get; set; }

    [JsonProperty("default_iconid")]
    public string? DefaultIconId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }
    #endregion

    #region Components
    [JsonProperty("mappings")]
    public IList<IconMapping>? IconMappings { get; set; }

    #endregion

    #region Constructors

    public IconMap(IList<IconMapping> mappings, string defaultIconId, string name)
    {
        IconMappings = mappings;
        DefaultIconId = defaultIconId;
        Name = name;
    }

    public IconMap()
    {
    }
    #endregion

}
