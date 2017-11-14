using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Spike.WebAdmin.API.Entities
{
  public static class WorkerOperatorsContextExtentions
  {
    public static void CreateDatabase(this WorkerOperatorsContext context)
    {
      using (var conn = context.Connection)
      {
        conn.Open();

        var statement = @"CREATE TABLE Operator
                          (Id   Text UNIQUE primary key,
                           Code Text not null,
                           Description Text not null)";
        conn.Execute(statement);
      }
    }


    public static void SeedData(this WorkerOperatorsContext context)
    {
      DeleteAllDatas(context);

      var dumpDatas = GetDumpDatas();

      foreach (var product in dumpDatas)
      {
        context.SaveWorkerOperator(product);
      }
    }

    private static List<WorkerOperator> GetDumpDatas()
    {
      return new List<WorkerOperator>
      {
        new WorkerOperator
        {
          Id = new Guid("a1da1d8e-1988-4634-b538-a01709477b77").ToString(),
          Code = "00001",
          Description = "SuperUser"
        },
        new WorkerOperator
        {
          Id = new Guid("1325360c-8253-473a-a20f-55c269c20407").ToString(),
          Code = "00002",
          Description = "Administrator"
        },
        new WorkerOperator
        {
          Id = new Guid("e57b605f-8b3c-4089-b672-6ce9e6d6c23f").ToString(),
          Code = "00003",
          Description = "User"
        }
      };
    }

    private static void DeleteAllDatas(WorkerOperatorsContext context)
    {
      context.DeleteAll();
    }
  }
}