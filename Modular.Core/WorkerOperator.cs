using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spike.WebAdmin.API.Entities
{
  public class WorkerOperator
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    public string Description { get; set; }
  }
}