﻿using Newtonsoft.Json;
using Zabbix.Core;
using Zabbix.Entities;
using Zabbix.Filter;
using Zabbix.Services.CrudServices;

namespace Zabbix.Services;

public class RegexObjectService : CrudService<RegexObject, RegexObjectFilterOptions, RegexObjectService.RegexObjectResult>
{
    public RegexObjectService(ICore core)
        : base(core, "regexp")
    {
    }

    public class RegexObjectResult : BaseResult
    {
        [JsonProperty("regexpids")]
        public override IList<string>? Ids { get; set; }
    }
}

public class RegexObjectFilterOptions : FilterOptions
{
    [JsonProperty("regexpids")]
    public object? RegularExpressionIds { get; set; }

    [JsonProperty("selectExpressions")]
    public ZabbixQuery? SelectExpressions { get; set; }
}

public enum RegexObjectInclude
{
}
