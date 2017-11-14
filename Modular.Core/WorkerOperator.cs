using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spike.WebAdmin.API.Entities
{
  [Table("Operator")]
  public class WorkerOperator
  {
    //string -> Guid is not supported by Sqlite
    [Key]
    public string Id { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    public string Description { get; set; }
  }
}