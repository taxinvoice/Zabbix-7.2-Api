using Zabbix.Services;

namespace Zabbix.Core
{
    public interface IZabbixCore: ICore
    {
        ActionService Actions { get; }
        AlertService Alerts { get; }
        ApiInfoService ApiInfo { get; }
        AuditLogService AuditLogs { get; }
        AuthenticationService Authentication { get; }
        AutoRegistrationService AutoRegistration { get; }
        ConfigurationService Configuration { get; }
        ConnectorService Connectors { get; }
        CorrelationService Correlations { get; }
        DashboardService Dashboards { get; }
        DiscoveredHostService DiscoveredHosts { get; }
        DiscoveredServiceService DiscoveredServices { get; }
        DiscoveryCheckService DiscoveryChecks { get; }
        DiscoveryRuleService DiscoveryRules { get; }
        EventService Events { get; }
        GraphItemService GraphItems { get; }
        GraphPrototypeService GraphPrototypes { get; }
        GraphService Graphs { get; }
        HighAvailabilityNodeService HighAvailabilityNodes { get; }
        HistoryService History { get; }
        HostGroupService HostGroups { get; }
        HostInterfaceService HostInterfaces { get; }
        HostPrototypeService HostPrototypes { get; }
        HostService Hosts { get; }
        HousekeepingService Housekeeping { get; }
        IconMapService IconMaps { get; }
        ImageService Images { get; }
        ItemPrototypeService ItemPrototypes { get; }
        ItemService Items { get; }
        LldRuleService LldRules { get; }
        MaintenanceService Maintenance { get; }
        MapService Maps { get; }
        MediaTypeService MediaTypes { get; }
        ProblemService Problems { get; }
        ProxyService Proxies { get; }
        RegexObjectService Regex { get; }
        ReportService Reports { get; }
        RoleService Roles { get; }
        ScriptService Scripts { get; }
        ServiceService Services { get; }
        SettingsService Settings { get; }
        SlaService SLAs { get; }
        TaskService Tasks { get; }
        TemplateDashboardService TemplateDashboards { get; }
        TemplateService Templates { get; }
        TokenService Tokens { get; }
        TrendService Trends { get; }
        TriggerPrototypeService TriggerPrototypes { get; }
        TriggerService Triggers { get; }
        UserGroupService UserGroups { get; }
        UserMacroService UserMacros { get; }
        UserService Users { get; }
        ValueMapService ValueMaps { get; }
        WebScenarioService WebScenarios { get; }
    }
}