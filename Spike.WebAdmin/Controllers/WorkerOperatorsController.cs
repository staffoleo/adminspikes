using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Modular.Core;
using Spike.WebAdmin.API.Entities;
using Spike.WebAdmin.API.Models;
using Spike.WebAdmin.API.Services;
using System.Linq;
using Spike.WebAdmin.GenderModule.Services;

namespace Spike.WebAdmin.API.Controllers
{
  [Route("api/workeroperators")]
  public class WorkerOperatorsController : Controller
  {
    private readonly IWorkerOperatorsRepository _workerOperatorsRepository;
    private readonly List<IPlugins<WorkerOperator>> _plugins;

    public WorkerOperatorsController(IWorkerOperatorsRepository workerOperatorsRepository)
    {
      _workerOperatorsRepository = workerOperatorsRepository;
      _plugins = new List<IPlugins<WorkerOperator>>() {new GenderPlugin(), new BirthdayPlugin()};
    }

    [HttpGet]
    public IActionResult GetWorkerOperators()
    {
      var workerOperatorsFromRepo = _workerOperatorsRepository.GetWorkerOperators();
      return Ok(workerOperatorsFromRepo);
    }

    [HttpGet("{id}", Name = "GetWorkerOperator")]
    public IActionResult GetWorkerOperator(Guid id)
    {
      var workerOperator = _workerOperatorsRepository.GetWorkerOperator(id);

      var obList = _plugins.Select(module =>new { module = module.Get(workerOperator)}).ToList();

      var dictionary = new Dictionary<string, object>();
      foreach (var plugin in _plugins)
      {
        var result = plugin.Get(workerOperator);
        workerOperator = result.Entity;
        dictionary.Add(plugin.Name, result.PluginResult);
      }


      if (workerOperator == null)
      {
        return NotFound();
      }

      return Ok(new
      {
        workerOperator = workerOperator,
        obList = dictionary
      });
    }

    [HttpPost]
    public IActionResult CreateWorkerOperator([FromBody] WorkerOperatorForCreationDto workerOperator)
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
      return CreatedAtRoute("GetWorkerOperator", new {id = workerOperatorToReturn.Id, workerOperatorToReturn});
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
  }
}