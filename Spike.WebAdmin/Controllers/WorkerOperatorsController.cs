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
using Newtonsoft.Json;
using Spike.WebAdmin.BirthdayModule;
using Spike.WebAdmin.GenderModule;

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
      _plugins = new List<IPlugins<WorkerOperator>> {new GenderPlugin(), new BirthdayPlugin()};
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

      if (workerOperator == null)
      {
        return NotFound();
      }

      var dictionary = new Dictionary<string, object> {{"workerOperator", workerOperator}};
      foreach (var plugin in _plugins)
      {
        var result = plugin.Get(workerOperator);
        workerOperator = result.Entity;
        dictionary.Add(plugin.Name, result.PluginResult);
      }
      
      return Ok(dictionary);
    }

    [HttpPost]
    public IActionResult CreateWorkerOperator([FromBody] Dictionary<string, object> workerOperator)
    {
      if (workerOperator == null || !workerOperator.ContainsKey("workerOperator"))
      {
        BadRequest();
      }

      var workerOperatorForCreationDto = JsonConvert.DeserializeObject<WorkerOperatorForCreationDto>(workerOperator["workerOperator"].ToString());
      var workerOperatorEntity = Mapper.Map<WorkerOperator>(workerOperatorForCreationDto);

      var result = new List<Composite<WorkerOperator>>();

      foreach (var plugin in _plugins)
      {
        Type t = plugin.GetType();
        var composite = new Composite<WorkerOperator>
        {
          Entity = workerOperatorEntity,
          PluginResult = JsonConvert.DeserializeObject(workerOperator[plugin.Name].ToString(), t)
        };

        result.Add(composite);
      }

      _workerOperatorsRepository.AddWorkerOperator(workerOperatorEntity);
      
      if (!_workerOperatorsRepository.Save())
      {
        throw new Exception("Creating a workerOperator failed on save");
      }

      var workerOperatorToReturn = Mapper.Map<WorkerOperatorDto>(workerOperatorEntity);
      return CreatedAtRoute("GetWorkerOperator", new {id = workerOperatorToReturn.Id, workerOperatorToReturn});
    }
  }
}