using System;

namespace Spike.WebAdmin.API.Entities
{
  public static class WorkerOperatorsContextExtentions
  {
    public static void SeedData(this WorkerOperatorsContext context)
    {
      foreach (var item in context.WorkerOperators)
      {
        context.WorkerOperators.Remove(item);
      }

      context.WorkerOperators.Add(new WorkerOperator
      {
        Id = new Guid("a1da1d8e-1988-4634-b538-a01709477b77"),
        Code = "00001",
        Description = "SuperUser"
      });

      context.WorkerOperators.Add(new WorkerOperator
      {
        Id = new Guid("1325360c-8253-473a-a20f-55c269c20407"),
        Code = "00002",
        Description = "Administrator"
      });

      context.WorkerOperators.Add(new WorkerOperator
      {
        Id = new Guid("e57b605f-8b3c-4089-b672-6ce9e6d6c23f"),
        Code = "00003",
        Description = "User"
      });
    }
  }
}