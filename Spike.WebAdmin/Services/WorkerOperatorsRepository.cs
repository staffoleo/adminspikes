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
      return _context.WorkerOperators.OrderBy(x => x.Code);
    }

    public WorkerOperator GetWorkerOperator(Guid workerOperatorId)
    {
      return _context.WorkerOperators.FirstOrDefault(a => a.Id == workerOperatorId);
    }

    public void AddWorkerOperator(WorkerOperator workerOperator)
    {
      workerOperator.Id = Guid.NewGuid();
      _context.WorkerOperators.Add(workerOperator);
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
      return _context.WorkerOperators.Any(a => a.Id == workerOperatorId);
    }

    public bool Save()
    {
      return true;
    }
  }
}
