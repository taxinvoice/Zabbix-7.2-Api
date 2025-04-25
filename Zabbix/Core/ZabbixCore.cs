﻿using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Zabbix.Entities;
using Zabbix.Services;

namespace Zabbix.Core;

public class ZabbixCore : IZabbixCore
{
    private volatile string? _authenticationToken = "";
    private readonly HttpClient _httpClient;
    private User? _loggedInUser;
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly string _url;
    
    /// <summary>
    /// Whether to log HTTP requests and responses
    /// </summary>
    public bool EnableLogging { get; set; } = true;
    
    /// <summary>
    /// Action that handles log messages when logging is enabled
    /// </summary>
    public Action<string>? Logger { get; set; }

    public ZabbixCore(string url, string username, string password, string? authenticationToken = null)
    {
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        if (url == null)
        {
            throw new ArgumentNullException(nameof(url));
        }

        if (username == null)
        {
            throw new ArgumentNullException(nameof(username));
        }

        _serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };

        // Todo: do this using reflection
        _url = url;
        _httpClient = new HttpClient();
        GraphItems = new GraphItemService(this);
        AutoRegistration = new (this);
        Correlations = new CorrelationService(this);
        Hosts = new HostService(this);
        HostGroups = new HostGroupService(this);
        WebScenarios = new WebScenarioService(this);
        Users = new UserService(this);
        Triggers = new TriggerService(this);
        Problems = new ProblemService(this);
        Reports = new ReportService(this);
        HostInterfaces = new HostInterfaceService(this);
        Alerts = new AlertService(this);
        Events = new EventService(this);
        Actions = new ActionService(this);
        Authentication = new AuthenticationService(this);
        UserMacros = new UserMacroService(this);
        Roles = new RoleService(this);
        Items = new ItemService(this);
        AuditLogs = new AuditLogService(this);
        Proxies = new ProxyService(this);
        ValueMaps = new ValueMapService(this);
        DiscoveredHosts = new DiscoveredHostService(this);
        DiscoveryChecks = new DiscoveryCheckService(this);
        DiscoveredServices = new DiscoveredServiceService(this);
        DiscoveryRules = new DiscoveryRuleService(this);
        TriggerPrototypes = new TriggerPrototypeService(this);
        Trends = new TrendService(this);
        LldRules = new LldRuleService(this);
        UserGroups = new UserGroupService(this);
        GraphPrototypes = new GraphPrototypeService(this);
        HighAvailabilityNodes = new HighAvailabilityNodeService(this);
        History = new HistoryService(this);
        HostPrototypes = new HostPrototypeService(this);
        Housekeeping = new HousekeepingService(this);
        IconMaps = new IconMapService(this);
        Images = new ImageService(this);
        ItemPrototypes = new ItemPrototypeService(this);
        Maintenance = new MaintenanceService(this);
        MediaTypes = new MediaTypeService(this);
        Regex = new RegexObjectService(this);
        Scripts = new ScriptService(this);
        Settings = new SettingsService(this);
        SLAs = new SlaService(this);
        TemplateDashboards = new TemplateDashboardService(this);
        Templates = new TemplateService(this);
        Tokens = new TokenService(this);
        Configuration = new ConfigurationService(this);
        Services = new ServiceService(this);
        Tasks = new TaskService(this);
        Connectors = new ConnectorService(this);
        ApiInfo = new ApiInfoService(this);
        Dashboards = new DashboardService(this);
        Graphs = new GraphService(this);
        Maps = new MapService(this);

        if (string.IsNullOrWhiteSpace(authenticationToken))
        {
            Authenticate(username, password);
        }
        else
        {
            _authenticationToken = authenticationToken;
        }
    }

    public T SendRequest<T>(object? @params, string method)
    {
        lock (_httpClient)
        {
            string? token = CheckAndGetToken();
            return SendRequest<T>(@params, method, token);
        }
    }

    public virtual T SendRequest<T>(object? @params, string method, string? token)
    {
        lock (_httpClient)
        {
            var request = GetRequest(@params, method, token);
            string json = JsonConvert.SerializeObject(request, _serializerSettings);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");

            // add bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Log request if enabled
            if (EnableLogging && Logger != null)
            {
                Logger($"REQUEST to {_url}:\n{json}");
            }

            var response = _httpClient.PostAsync(_url, requestData).Result;
            string responseData = response.Content.ReadAsStringAsync().Result;
            
            // Log response if enabled
            if (EnableLogging && Logger != null)
            {
                Logger($"RESPONSE from {_url} (Status: {response.StatusCode}):\n{responseData}");
            }
            
            response.EnsureSuccessStatusCode();
            var ret = HandleResponse<T>(request.Id, responseData);

            return ret;
        }
    }

    public async Task<T> SendRequestAsync<T>(object? @params, string method)
    {
        string? token = CheckAndGetToken();
        return await SendRequestAsync<T>(@params, method, token);
    }

    public virtual async Task<T> SendRequestAsync<T>(object? @params, string method, string? token)
    {
        var request = GetRequest(@params, method, token);
        string json = JsonConvert.SerializeObject(request, _serializerSettings);
        var requestData = new StringContent(json, Encoding.UTF8, "application/json");

        // add bearer token
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        // Log request if enabled
        if (EnableLogging && Logger != null)
        {
            Logger($"REQUEST to {_url}:\n{json}");
        }

        var response = await _httpClient.PostAsync(_url, requestData).ConfigureAwait(false);
        string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        
        // Log response if enabled
        if (EnableLogging && Logger != null)
        {
            Logger($"RESPONSE from {_url} (Status: {response.StatusCode}):\n{responseData}");
        }
        
        response.EnsureSuccessStatusCode();
        return HandleResponse<T>(request.Id, responseData);
    }

    public void Dispose()
    {
        Users.Logout();
        _httpClient.Dispose();
    }

    private void Authenticate(string user, string password)
    {
        _loggedInUser = Users.Login(user, password);
        _authenticationToken = _loggedInUser.SessionId;
    }

    private T HandleResponse<T>(string requestId, string responseData)
    {
        var response = JsonConvert.DeserializeObject<Response<T>>(responseData, _serializerSettings);

        if (response == null)
        {
            throw new NullReferenceException($"Object Deserialization is null for {responseData} with Id {requestId}");
        }

        if (response.Error != null)
        {
            throw new Exception(response.Error.Message, new Exception($"{response.Error.Data} - code:{response.Error.Code}"));
        }

        if (response.Result == null)
        {
            throw new NullReferenceException($"Result is null for {responseData} with Id {requestId}. Possibly an error with Deserialization");
        }

        if (response.Id != requestId)
        {
            throw new Exception($"The response id ({response.Id}) does not match the request id ({requestId})");
        }

        return response.Result;
    }

    private Request GetRequest(object? @params, string method, string? authenticationToken)
    {
        return new Request
        {
            Method = method,
            Params = @params,
            Id = Guid.NewGuid().ToString()
        };
    }

    private string? CheckAndGetToken()
    {
        string? token = _authenticationToken;
        if (token == "")
        {
            throw new InvalidOperationException("This zabbix core isn't authenticated.");
        }

        return token;
    }

    #region Services
    public ActionService Actions { get; }
    public AlertService Alerts { get; }
    public AuditLogService AuditLogs { get; }
    public AuthenticationService Authentication { get; }
    public AutoRegistrationService AutoRegistration { get; }
    public ConfigurationService Configuration { get; }
    public CorrelationService Correlations { get; }
    public DiscoveredHostService DiscoveredHosts { get; }
    public DiscoveryCheckService DiscoveryChecks { get; }
    public DiscoveredServiceService DiscoveredServices { get; }
    public DiscoveryRuleService DiscoveryRules { get; }
    public EventService Events { get; }
    public GraphItemService GraphItems { get; }
    public GraphPrototypeService GraphPrototypes { get; }
    public HighAvailabilityNodeService HighAvailabilityNodes { get; }
    public HistoryService History { get; }
    public HostGroupService HostGroups { get; }
    public HostInterfaceService HostInterfaces { get; }
    public HostPrototypeService HostPrototypes { get; }
    public HostService Hosts { get; }
    public HousekeepingService Housekeeping { get; }
    public IconMapService IconMaps { get; }
    public ImageService Images { get; }
    public ItemPrototypeService ItemPrototypes { get; }
    public ItemService Items { get; }
    public LldRuleService LldRules { get; }
    public MaintenanceService Maintenance { get; }
    public MediaTypeService MediaTypes { get; }
    public ProblemService Problems { get; }
    public ProxyService Proxies { get; }
    public RegexObjectService Regex { get; }
    public ReportService Reports { get; }
    public RoleService Roles { get; }
    public ScriptService Scripts { get; }
    public ServiceService Services { get; }
    public SettingsService Settings { get; }
    public SlaService SLAs { get; }
    public DashboardService Dashboards { get; }
    public TaskService Tasks { get; }
    /// <summary>
    /// Only Available on Zabbix Version >= 6.4
    /// </summary>
    public ConnectorService Connectors { get; }
    public TemplateDashboardService TemplateDashboards { get; }
    public TemplateService Templates { get; }
    public TokenService Tokens { get; }
    public TrendService Trends { get; }
    public TriggerPrototypeService TriggerPrototypes { get; }
    public TriggerService Triggers { get; }
    public UserGroupService UserGroups { get; }
    public UserMacroService UserMacros { get; }
    public UserService Users { get; }
    public ValueMapService ValueMaps { get; }
    public WebScenarioService WebScenarios { get; }
    public ApiInfoService ApiInfo { get; }
    public GraphService Graphs { get; }
    public MapService Maps { get; }

    #endregion
}
