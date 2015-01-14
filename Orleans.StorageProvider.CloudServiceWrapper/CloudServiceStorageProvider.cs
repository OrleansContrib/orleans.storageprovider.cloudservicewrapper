using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.ServiceRuntime;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Storage;

namespace Orleans.StorageProvider.CloudServiceWrapper
{
  public abstract class CloudServiceStorageProvider<T> : IStorageProvider where T : IStorageProvider, new()
  {
    private const string _serviceConfigurationSettingPrefix = "ServiceConfigurationSetting:";

    private readonly T _storageProvider = new T();

    public async Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
    {
      await _storageProvider.ClearStateAsync(grainType, grainReference, grainState);
    }

    public async Task Close()
    {
      await _storageProvider.Close();
    }

    public OrleansLogger Log
    {
      get { return _storageProvider.Log; }
    }

    public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
    {
      await _storageProvider.ReadStateAsync(grainType, grainReference, grainState);
    }

    public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
    {
      await _storageProvider.WriteStateAsync(grainType, grainReference, grainState);
    }

    public async Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
    {
      Dictionary<string, string> properties = new Dictionary<string, string>();

      foreach(var configProperty in config.Properties)
      {
        var propertyName = configProperty.Key;
        var propertyValue = configProperty.Value;

        if(propertyValue.StartsWith(_serviceConfigurationSettingPrefix))
        {
          string serviceConfigurationSettingName = propertyValue.Substring(_serviceConfigurationSettingPrefix.Length);
          propertyValue = RoleEnvironment.GetConfigurationSettingValue(serviceConfigurationSettingName);
        }

        properties.Add(propertyName, propertyValue);
      }

      await _storageProvider.Init(name, providerRuntime, new BasicProviderConfiguration(config.Name, config.Children, properties));
    }

    public string Name
    {
      get { return _storageProvider.Name; }
    }
  }
}
