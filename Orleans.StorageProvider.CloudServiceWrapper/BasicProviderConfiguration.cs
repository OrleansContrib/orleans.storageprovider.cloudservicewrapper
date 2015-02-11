using System.Collections.Generic;
using Orleans.Providers;

namespace Orleans.StorageProvider.CloudServiceWrapper
{
  internal class BasicProviderConfiguration : IProviderConfiguration
  {
    private readonly IList<IProvider> _children;
    private readonly IDictionary<string, string> _properties;

    public BasicProviderConfiguration(string name, IList<IProvider> children, IDictionary<string, string> properties)
    {
      Name = name;
      _children = children;
      _properties = properties;
    }

    public IList<IProvider> Children
    {
      get { return _children == null ? new List<IProvider>() : new List<IProvider>(_children); }
    }

    public string Name { get; private set; }

    public IDictionary<string, string> Properties
    {
      get { return _properties == null ? new Dictionary<string, string>() : new Dictionary<string, string>(_properties); }
    }
  }
}
