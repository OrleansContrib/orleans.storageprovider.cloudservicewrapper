using Orleans.Storage;

namespace Orleans.StorageProvider.CloudServiceWrapper
{
  /// <summary>
  /// Wraps the AzureTableStorage with the CloudServiceStorageProvider.
  /// This specific class is necessary because generics cannot be configured in the Orleans Configurations.
  /// </summary>
  public class CloudServiceAzureTableStorage : CloudServiceStorageProvider<AzureTableStorage> { }
}
