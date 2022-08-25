using Microsoft.Extensions.Hosting;
using iot.Identity.Api;
using TechTalk.SpecFlow;

namespace UserApi.Tests.Hooks;

[Binding]
public sealed class Hooks
{
    //private static IHost _host;

    //[BeforeTestRun]
    //public static void BeforeTestRun()
    //{
    //    _host = Program.CreateHostBuilder(null).Build();

    //    _host.Start();
    //}

    //[AfterTestRun]
    //public static void AfterTestRun()
    //{
    //    _host.StopAsync().Wait();
    //}
}
