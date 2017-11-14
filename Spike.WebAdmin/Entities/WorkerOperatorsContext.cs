using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Microsoft.Data.Sqlite;

namespace Spike.WebAdmin.API.Entities
{
  public class WorkerOperatorsContext
  {
    public static readonly string DbFile = $"{Environment.CurrentDirectory}\\WorkerOperatorDb.sqlite";
    private readonly string _connectionString = $"Data Source= {DbFile} ";

    public IDbConnection Connection => new SqliteConnection(_connectionString);

    public IList<WorkerOperator> WorkerOperators { get; set; }

    public WorkerOperatorsContext()
    {
      if (!File.Exists(DbFile))
        this.CreateDatabase();
    }

    public void SaveWorkerOperator(WorkerOperator workerOperator)
    {
      using (var cnn = Connection)
      {
        cnn.Open();
        cnn.Insert(workerOperator);
      }
    }

    public IEnumerable<WorkerOperator> GetWorkerOperators()
    {
      using (var cnn = Connection)
      {
        cnn.Open();
        return cnn.Find<WorkerOperator>();
      }

    }

    public WorkerOperator GetWorkerOperator(string workerOperatorId)
    {
      using (var cnn = Connection)
      {
        cnn.Open();
        return cnn.Get(new WorkerOperator {Id = workerOperatorId});
      }
    }

    public void DeleteAll()
    {
      using (var cnn = Connection)
      {
        cnn.Open();
        cnn.BulkDelete<WorkerOperator>();
      }
    }
  }
}
