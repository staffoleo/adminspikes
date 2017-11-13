using System;

namespace Modular.Core
{
  public class Composite<T>
  {
    public T Entity { get; set; }
    public object PluginResult { get; set; }
  }

  public interface IPlugins<T>
  {
    string Name { get; }

    Composite<T> Get(T entity);
  }
}