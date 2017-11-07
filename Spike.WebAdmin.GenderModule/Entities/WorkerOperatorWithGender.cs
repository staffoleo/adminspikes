using System.ComponentModel.DataAnnotations;
using Spike.WebAdmin.API.Entities;

namespace Spike.WebAdmin.GenderModule.Entities
{
  public class WorkerOperatorWithGender
  {
    //public WorkerOperator WorkerOperator { get; set; }

    [Required]
    public string Gender { get; set; }
  }
}