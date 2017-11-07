using Microsoft.AspNetCore.Mvc;
using Spike.WebAdmin.TestModule.Service;

namespace Spike.WebAdmin.TestModule.Controllers
{
  [Route("api/test")]
  public class TestController : Controller
  {
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
      _testService = testService;
    }

    [HttpGet]
    public IActionResult GetTest()
    {
      var test = _testService.Test();
      return Ok(test);
    }
  }
}
