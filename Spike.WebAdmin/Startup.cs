using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Modular.Core;
using Spike.WebAdmin.API.Entities;
using Spike.WebAdmin.API.Models;
using Spike.WebAdmin.API.Services;

namespace Spike.WebAdmin.API
{
  public class Startup
  {
    private readonly IHostingEnvironment _hostingEnvironment;
    public IConfiguration Configuration { get; }
    private IList<ModuleInfo> _modules = new List<ModuleInfo>();

    public Startup(IConfiguration configuration, IHostingEnvironment env)
    {
      _hostingEnvironment = env;
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      _modules = LoadModules();

      var mvcBuilder = services.AddMvc();
      InitializeModules(services, mvcBuilder);

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

      foreach (var module in _modules)
      {
        var wwwrootDir = new DirectoryInfo(Path.Combine(module.Path, "wwwroot"));
        if (!wwwrootDir.Exists)
        {
          continue;
        }

        app.UseStaticFiles(new StaticFileOptions()
        {
          FileProvider = new PhysicalFileProvider(wwwrootDir.FullName),
          RequestPath = new PathString("/" + module.SortName)
        });
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

    private IList<ModuleInfo> LoadModules()
    {
      var modules = new List<ModuleInfo>();
      var moduleRootFolder = _hostingEnvironment.ContentRootPath;
      var compiledPluginFolder = new DirectoryInfo(Path.Combine(moduleRootFolder, @"..\compiledPlugins"));

      foreach (var file in compiledPluginFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
      {
        Assembly assembly;
        try
        {
          assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
        }
        catch (FileLoadException ex)
        {
          if (ex.Message == "Assembly with same name is already loaded")
          {
            continue;
          }
          throw;
        }

        modules.Add(new ModuleInfo { Name = file.Name, Assembly = assembly, Path = file.FullName });
      }

      return modules;
    }

    private void InitializeModules(IServiceCollection services, IMvcBuilder mvcBuilder)
    {
      foreach (var module in _modules)
      {
        // Register controller from modules
        mvcBuilder.AddApplicationPart(module.Assembly);

        // Register dependency in modules
        var moduleInitializerType =
          module.Assembly.GetTypes().FirstOrDefault(x => typeof(IModuleInitializer).IsAssignableFrom(x));
        if (moduleInitializerType != null && moduleInitializerType != typeof(IModuleInitializer))
        {
          var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
          moduleInitializer.Init(services);
        }
      }
    }
  }
}


