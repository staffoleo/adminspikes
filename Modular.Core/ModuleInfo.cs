using System.Linq;
using System.Reflection;

namespace Modular.Core
{
  public class ModuleInfo
  {
    public string Name { get; set; }

    public Assembly Assembly { get; set; }

    public string SortName => Name.Split('.').Last();

    public string Path { get; set; }
  }
}
