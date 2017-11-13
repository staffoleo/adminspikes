using System;
using Modular.Core;
using Spike.WebAdmin.API.Entities;

namespace Spike.WebAdmin.BirthdayModule
{
  public class BirthdayPlugin : IPlugins<WorkerOperator>
  {
    public string Name => nameof(BirthdayPlugin);

    Composite<WorkerOperator> IPlugins<WorkerOperator>.Get(WorkerOperator entity)
    {      
      return new Composite<WorkerOperator>()
      {
        Entity = entity,
        PluginResult = new {Birthday = DateTime.Now}
      };
    }    
  }
}