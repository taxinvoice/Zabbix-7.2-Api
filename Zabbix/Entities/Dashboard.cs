﻿using Newtonsoft.Json;

namespace Zabbix.Entities;

public class DashboardField : BaseEntity
{
    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("value")]
    public string? Value { get; set; }
}

public class DashboardPage : BaseEntity
{
    [JsonProperty("dashboard_pageid")]
    public override string? EntityId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("display_period")]
    public string? DisplayPeriod { get; set; }

    [JsonProperty("widgets")]
    public IList<DashboardWidget>? Widgets { get; set; }
}

public class Dashboard : BaseEntity
{
    [JsonProperty("dashboardid")]
    public override string? EntityId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("templateid")]
    public string? Templateid { get; set; }

    [JsonProperty("display_period")]
    public string? DisplayPeriod { get; set; }

    [JsonProperty("auto_start")]
    public string? AutoStart { get; set; }

    [JsonProperty("uuid")]
    public string? Uuid { get; set; }

    [JsonProperty("pages")]
    public IList<DashboardPage>? Pages { get; set; }
    [JsonProperty("users")]
    public IList<DashboardUser>? Users { get; set; }
    [JsonProperty("userGroups")]
    public IList<DashboardUserGroup>? UserGroups { get; set; }
}

public class DashboardWidget : BaseEntity
{
    [JsonProperty("widgetid")]
    public override string? EntityId { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("x")]
    public string? X { get; set; }

    [JsonProperty("y")]
    public string? Y { get; set; }

    [JsonProperty("width")]
    public string? Width { get; set; }

    [JsonProperty("height")]
    public string? Height { get; set; }

    [JsonProperty("view_mode")]
    public string? ViewMode { get; set; }

    [JsonProperty("fields")]
    public IList<DashboardField>? Fields { get; set; }
}

public class DashboardUserGroup
{
    [JsonProperty("usrgrpid")]
    public string? UserGroupId { get; set; }
    [JsonProperty("permission")]
    public int? Permission { get; set; }
}

public class DashboardUser
{
    [JsonProperty("userid")]
    public string? UserId { get; set; }
    [JsonProperty("permission")]
    public int? Permission { get; set; }
}
