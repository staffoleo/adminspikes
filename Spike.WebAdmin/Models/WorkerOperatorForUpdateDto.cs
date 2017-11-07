using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spike.WebAdmin.API.Models
{
  public class WorkerOperatorForUpdateDto
  {
    [Required(ErrorMessage = "You should fill out a code")]
    public string Code { get; set; }

    [Required(ErrorMessage = "You should fill out a description")]
    public virtual string Description { get; set; }

    public bool IsEnabled { get; set; }

    public DateTimeOffset ValidFrom { get; set; }

    public DateTimeOffset ValidTo { get; set; }
  }
}
