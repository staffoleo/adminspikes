using Microsoft.Extensions.DependencyInjection;

namespace Modular.Core
{
  public interface IModuleInitializer
  {
    void Init(IServiceCollection serviceCollection);
  }
}
