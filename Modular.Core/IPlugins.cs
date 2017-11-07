namespace Modular.Core
{
  public class Composite<T>
  {
    public T Entity { get; set; }
    public object PluginResult { get; set; }
  }

  public interface IPlugins<T>
  {
    Composite<T> Get(T entity);
    string Name { get;  }
  }
}