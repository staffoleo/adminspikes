using System;
using System.Collections.Generic;
using Spike.WebAdmin.API.Entities;

namespace Spike.WebAdmin.API.Services
{
  public interface IWorkerOperatorsRepository
  {
    IEnumerable<WorkerOperator> GetWorkerOperators();
    WorkerOperator GetWorkerOperator(Guid workerOperatorId);

    void AddWorkerOperator(WorkerOperator workerOperator);
    void DeleteWorkerOperator(WorkerOperator workerOperator);

    void UpdateWorkerOperator(WorkerOperator workerOperator);

    bool WorkerOperatorExists(Guid workerOperatorId);

    bool Save();
  }
}