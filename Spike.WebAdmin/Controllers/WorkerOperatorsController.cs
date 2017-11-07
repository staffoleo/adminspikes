using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Spike.WebAdmin.API.Entities;
using Spike.WebAdmin.API.Models;
using Spike.WebAdmin.API.Services;

namespace Spike.WebAdmin.API.Controllers
{
  [Route("api/workeroperators")]
  public class WorkerOperatorsController : Controller
  {
    private readonly IWorkerOperatorsRepository _workerOperatorsRepository;

    public WorkerOperatorsController(IWorkerOperatorsRepository workerOperatorsRepository)
    {
      _workerOperatorsRepository = workerOperatorsRepository;
    }
    
    [HttpGet]
    public IActionResult GetWorkerOperators()
    {
      var workerOperatorsFromRepo = _workerOperatorsRepository.GetWorkerOperators();
      return Ok(workerOperatorsFromRepo);
    }

    [HttpGet("{id}", Name = "GetWorkerOperator")]
    public IActionResult GetWorkerOperators(Guid id)
    {
      var workerOperatorFromRepo = _workerOperatorsRepository.GetWorkerOperator(id);
      if (workerOperatorFromRepo == null)
      {
        return NotFound();
      }

      return Ok(workerOperatorFromRepo);
    }

    [HttpPost]
    public IActionResult CreateWorkerOperator([FromBody] WorkerOperatorForCreationDto workerOperator )
    {
      if (workerOperator == null)
      {
        BadRequest();
      }

      var workerOperatorEntity = Mapper.Map<WorkerOperator>(workerOperator);
      _workerOperatorsRepository.AddWorkerOperator(workerOperatorEntity);


      if (!_workerOperatorsRepository.Save())
      {
        throw new Exception("Creating a workerOperator failed on save");
      }

      var workerOperatorToReturn = Mapper.Map<WorkerOperatorDto>(workerOperatorEntity);
      return CreatedAtRoute("GetWorkerOperator", new { id = workerOperatorToReturn.Id, workerOperatorToReturn });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteWorkerOperator(Guid id)
    {
      var workerOperatorFromRepo = _workerOperatorsRepository.GetWorkerOperator(id);
      if (workerOperatorFromRepo == null)
      {
        return NotFound();
      }

      _workerOperatorsRepository.DeleteWorkerOperator(workerOperatorFromRepo);

      if (!_workerOperatorsRepository.Save())
      {
        throw new Exception($"Deleting workerOperator {id} failed on save");
      }

      return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateWorkerOperator(Guid id, [FromBody] WorkerOperatorForUpdateDto workerOperator)
    {
      if (workerOperator == null)
      {
        return BadRequest();
      }

      var workerOperatorFromRepo = _workerOperatorsRepository.GetWorkerOperator(id);
      if (workerOperatorFromRepo == null)
      {
        return NotFound();
      }

      Mapper.Map(workerOperator, workerOperatorFromRepo);

      _workerOperatorsRepository.UpdateWorkerOperator(workerOperatorFromRepo);

      if (_workerOperatorsRepository.Save())
      {
        throw new Exception($"Updating WorkerOperator {id} failed on save.");
      }

      return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult PartiallyUpdateWorkerOperator(Guid id, [FromBody] JsonPatchDocument<WorkerOperatorForUpdateDto> patchDoc)
    {
      if (patchDoc == null)
      {
        return BadRequest();
      }

      var workerOperatorFromRepo = _workerOperatorsRepository.GetWorkerOperator(id);
      if (workerOperatorFromRepo == null)
      {
        return NotFound();
      }

      var workerOperatorToPatch = Mapper.Map<WorkerOperatorForUpdateDto>(workerOperatorFromRepo);
      patchDoc.ApplyTo(workerOperatorToPatch, ModelState);

      TryValidateModel(workerOperatorToPatch);

      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      Mapper.Map(workerOperatorToPatch, workerOperatorFromRepo);

      _workerOperatorsRepository.UpdateWorkerOperator(workerOperatorFromRepo);

      if (_workerOperatorsRepository.Save())
      {
        throw new Exception($"Patching workerOperator {id} failed on save.");
      }

      return NoContent();
    }

  }
}
