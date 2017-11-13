using System.ComponentModel.DataAnnotations;
using Spike.WebAdmin.API.Entities;

namespace Spike.WebAdmin.GenderModule.Entities
{
  public class WorkerOperatorWithGender
  {
    [Required]
    public string Gender { get; set; }
  }
}