using Moq;
using Zabbix.Services;

namespace ZabbixIntegrationTests
{
    [TestClass]
    public class MockTests
    {
        /// <summary>
        /// This test ensures that the service response can be mocked by the application's unit tests.
        /// </summary>
        [TestMethod]
        public async Task Request_Mock_CompilesAsync()
        {
            var coreMock = new Mock<ZabbixCore>("http://localhost", "", "", "anyToken");

            coreMock.Setup(x => x.SendRequestAsync<Host[]>(It.IsAny<object>(), "host.get", It.IsAny<string?>())).ReturnsAsync((object parameters, string method, string? token) =>
            {
                return [new() { Name = "Test-1" }, new() { Name = "Test-2" }];
            });

            var testHosts = await coreMock.Object.Hosts.GetAsync(new HostFilterOptions());
            
            Assert.AreEqual(2, testHosts.Count());
            Assert.AreEqual("Test-1", testHosts.First().Name);
            Assert.AreEqual("Test-2", testHosts.Last().Name);
        }

        /// <summary>
        /// This test ensures that the service response can be mocked by the application's unit tests.
        /// </summary>
        [TestMethod]
        public void Request_Mock_Compiles()
        {
            var coreMock = new Mock<ZabbixCore>("http://localhost", "", "", "anyToken");

            coreMock.Setup(x => x.SendRequest<Host[]>(It.IsAny<object>(), "host.get", It.IsAny<string?>())).Returns((object parameters, string method, string? token) =>
            {
                return [new() { Name = "Test-1" }, new() { Name = "Test-2" }];
            });

            var testHosts = coreMock.Object.Hosts.Get(new HostFilterOptions());

            Assert.AreEqual(2, testHosts.Count());
            Assert.AreEqual("Test-1", testHosts.First().Name);
            Assert.AreEqual("Test-2", testHosts.Last().Name);
        }
    }
}
