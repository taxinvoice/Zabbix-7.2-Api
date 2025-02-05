using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Zabbix.Services;

namespace ZabbixIntegrationTests
{
    [TestClass]
    public class HostTests : TestBase
    {


        [TestMethod]
        public void TestHostCycle()
        {
            Host host = new Host("TestHost"+Id, TestGroup);

            // create method expects a group ID only
            host.Groups.First().Name = null;
            
            TestCycle(host, "Hosts");
        }

        public override void SetUp()
        {
            SetUpTestGroup();
        }

        public override void CleanUp()
        {
            DestroyTestGroup();
        }

    }
}