using Modular.Core;
using Spike.WebAdmin.API.Entities;
using Spike.WebAdmin.GenderModule.Entities;

namespace Spike.WebAdmin.GenderModule
{
  public class GenderPlugin : IPlugins<WorkerOperator>
  {
    public string Name => nameof(GenderPlugin);

    Composite<WorkerOperator> IPlugins<WorkerOperator>.Get(WorkerOperator entity)
    {
      return new Composite<WorkerOperator>()
      {
        Entity = entity,
        PluginResult = new WorkerOperatorWithGender {Gender = "Male"}
      };
    }

    
  }
}