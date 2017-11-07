using System;
using System.Collections.Generic;
using System.Text;

namespace Spike.WebAdmin.TestModule.Service
{
  public class TestService : ITestService
  {
    public string Test()
    {
      return "This is a test!!!";
    }
  }
}
