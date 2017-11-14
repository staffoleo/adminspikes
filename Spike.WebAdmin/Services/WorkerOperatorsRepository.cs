using System;
using System.Collections.Generic;
using System.Linq;
using Spike.WebAdmin.API.Entities;

namespace Spike.WebAdmin.API.Services
{
  public class WorkerOperatorsRepository : IWorkerOperatorsRepository
  {
    private readonly WorkerOperatorsContext _context;

    public WorkerOperatorsRepository(WorkerOperatorsContext context)
    {
      _context = context;
    }

    public IEnumerable<WorkerOperator> GetWorkerOperators()
    {
      return _context.GetWorkerOperators();
    }

    public WorkerOperator GetWorkerOperator(Guid workerOperatorId)
    {
      return _context.GetWorkerOperator(workerOperatorId.ToString());
    }

    public void AddWorkerOperator(WorkerOperator workerOperator)
    {
      workerOperator.Id = Guid.NewGuid().ToString();
      _context.SaveWorkerOperator(workerOperator);
    }

    public void DeleteWorkerOperator(WorkerOperator workerOperator)
    {
      _context.WorkerOperators.Remove(workerOperator);
    }

    public void UpdateWorkerOperator(WorkerOperator workerOperator)
    {
      
    }

    public bool WorkerOperatorExists(Guid workerOperatorId)
    {
      return _context.WorkerOperators.Any(a => a.Id == workerOperatorId.ToString());
    }

    public bool Save()
    {
      return true;
    }
  }
}
