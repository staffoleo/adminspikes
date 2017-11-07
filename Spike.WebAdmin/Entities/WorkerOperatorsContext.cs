using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spike.WebAdmin.API.Entities
{
  public class WorkerOperatorsContext
  {
    public IList<WorkerOperator> WorkerOperators { get; set; }

    public WorkerOperatorsContext()
    {
        WorkerOperators = new List<WorkerOperator>();
    }
  }
}
