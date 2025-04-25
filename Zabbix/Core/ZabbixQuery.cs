using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Zabbix.Core;

public class ZabbixQuery
{
    public object Value { get; set; }

    public ZabbixQuery() => Value = "extend";

    public ZabbixQuery(string queryStr) => Value = queryStr == "extend" ? queryStr : new[] { queryStr };

    public ZabbixQuery(params string[] queryStr) => Value = queryStr;

    public ZabbixQuery(IEnumerable<string> queryStrings) => Value = queryStrings;

    public ZabbixQuery(object value) => Value = value;
}

public class ZabbixQueryConverter : JsonConverter<ZabbixQuery>
{
    public override ZabbixQuery? ReadJson(JsonReader reader, Type objectType, ZabbixQuery? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        object? obj = token.ToObject<object>();
        return obj == null ? null : new ZabbixQuery(obj);
    }

    public override void WriteJson(JsonWriter writer, ZabbixQuery? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            return;
        }

        var token = value.Value switch
        {
            string stringValue => new JValue(stringValue),
            string[] stringArray => JArray.FromObject(stringArray),
            IEnumerable<string> stringArray => JArray.FromObject(stringArray),
            _ => JToken.FromObject(value.Value)
        };

        token.WriteTo(writer);
    }
}
