using System;
using System.ComponentModel.DataAnnotations;

namespace Spike.WebAdmin.API.Entities
{
  public class WorkerOperator
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Code { get; set; }
    
    public string Password { get; set; }

    [Required]
    public string Description { get; set; }
    
    public bool IsEnabled { get; set; }

    public DateTimeOffset ValidFrom { get; set; }

    public DateTimeOffset ValidTo { get; set; }
  }
}
