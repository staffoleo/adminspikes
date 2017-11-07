using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spike.WebAdmin.API.Models
{
  public class WorkerOperatorForCreationDto
  {
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }
    
    public string Password { get; set; }

    public bool IsEnabled { get; set; }

    public DateTimeOffset ValidFrom { get; set; }

    public DateTimeOffset ValidTo { get; set; }
  }
}
