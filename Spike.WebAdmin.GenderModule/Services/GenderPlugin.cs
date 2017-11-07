using System;
using Modular.Core;
using Spike.WebAdmin.API.Entities;
using Spike.WebAdmin.GenderModule.Entities;

namespace Spike.WebAdmin.GenderModule.Services
{
  public class GenderPlugin : IPlugins<WorkerOperator>
  {
    Composite<WorkerOperator> IPlugins<WorkerOperator>.Get(WorkerOperator entity)
    {
      return new Composite<WorkerOperator>()
      {
        Entity = entity,
        PluginResult = new WorkerOperatorWithGender {Gender = "Male"}
      };
    }

    public string Name => "Gender";
  }

  public class BirthdayPlugin : IPlugins<WorkerOperator>
  {
    Composite<WorkerOperator> IPlugins<WorkerOperator>.Get(WorkerOperator entity)
    {
      if (true)
      {
        entity.Code = "****";
      }

      return new Composite<WorkerOperator>()
      {
        Entity = entity,
        PluginResult = new {Birthday = DateTime.Now}
      };
    }

    public string Name => "Birthday";
  }
}