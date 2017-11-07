using System;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;
using Spike.WebAdmin.TestModule.Service;

namespace Spike.WebAdmin.TestModule
{
  public class ModuleInitializer : IModuleInitializer
  {
    public void Init(IServiceCollection serviceCollection)
    {
      serviceCollection.AddTransient<ITestService, TestService>();
    }
  }
}
