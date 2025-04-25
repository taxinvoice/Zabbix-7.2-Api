﻿using Newtonsoft.Json;

namespace Zabbix.Entities;

public class Proxy : BaseEntity
{
    #region Properties

    [JsonProperty("proxyid")]
    public override string? EntityId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("lastaccess")]
    public string? LastAccess { get; set; }

    [JsonProperty("tls_connect")]
    public int? TlsConnect { get; set; }

    [JsonProperty("tls_accept")]
    public int? TlsAccept { get; set; }

    [JsonProperty("tls_issuer")]
    public string? TlsIssuer { get; set; }

    [JsonProperty("tls_subject")]
    public string? TlsSubject { get; set; }

    [JsonProperty("tls_psk_identity")]
    public string? TlsPskIdentity { get; set; }

    [JsonProperty("tls_psk")]
    public string? TlsPsk { get; set; }

    [JsonProperty("proxy_address")]
    public string? ProxyAddress { get; set; }

    [JsonProperty("auto_compress")]
    public int? AutoCompress { get; set; }

    [JsonProperty("operating_mode")]
    public int OperatingMode { get; set; } = 0;

    #endregion

    #region Components

    [JsonProperty("interface")]
    public IList<ProxyInterface>? Interfaces { get; set; }
    [JsonProperty("hosts")]
    public IList<Host>? Hosts { get; set; }

    #endregion

    #region Constructors

    public Proxy()
    {
    }

    #endregion
}

public class ProxyInterface
{
    #region Properties

    [JsonProperty("dns")]
    public string? Dns { get; set; }

    [JsonProperty("ip")]
    public string? Ip { get; set; }

    [JsonProperty("port")]
    public string? Port { get; set; }

    [JsonProperty("useip")]
    public int? UseIp { get; set; }

    #endregion

    #region Constructors

    public ProxyInterface(string dns, string ip, string port, int useIp)
    {
        Dns = dns;
        Ip = ip;
        Port = port;
        UseIp = useIp;
    }

    public ProxyInterface()
    {
    }

    #endregion

}
