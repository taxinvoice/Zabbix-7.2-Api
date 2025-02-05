using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZabbixIntegrationTests
{
    [TestClass]
    public class ProxyTests : TestBase
    {
        [TestMethod]
        public void TestProxyCycle()
        {
            Proxy p = new() {
                Name = "testproxy" + Id
            };
            TestCycle(p, "Proxies");
        }

        public override void SetUp()
        {
        }

        public override void CleanUp()
        {
        }
    }
}
