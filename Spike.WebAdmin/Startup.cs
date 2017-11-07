using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Spike.WebAdmin.API.Entities;
using Spike.WebAdmin.API.Models;
using Spike.WebAdmin.API.Services;

namespace Spike.WebAdmin
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      services.AddSingleton<WorkerOperatorsContext>();
      services.AddScoped<IWorkerOperatorsRepository, WorkerOperatorsRepository>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, WorkerOperatorsContext workerOperatorsContext)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      AutoMapper.Mapper.Initialize(cfg =>
      {
        cfg.CreateMap<WorkerOperator, WorkerOperatorDto>();
        cfg.CreateMap<WorkerOperatorForCreationDto, WorkerOperator>();
        cfg.CreateMap<WorkerOperatorForUpdateDto, WorkerOperator>();
        cfg.CreateMap<WorkerOperator, WorkerOperatorForUpdateDto>();
      });

      workerOperatorsContext.SeedData();

      app.UseMvc();
    }
  }
}


