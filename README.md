Azure Cloud Service Wrapper class for Orleans Storage Providers
===============================

This is an implementation that can be used to encapsulate any Orleans Storage provider to add the ability to configure the storage providers properties using the Azure service configuration file.

The CloudServiceAzureTableStorage class in the CloudServiceAzureTableStorage.cs file is an example of how the CloudServiceStorageProvider class can be used to enable this feature for the default AzureTableStorage class.

To configure this storage provider add the following to the OrleansConfiguration.xml:

```xml
<OrleansConfiguration xmlns="urn:orleans">
  <Globals>
    <StorageProviders>
      <Provider Type="OrleansSilo.CloudServiceAzureTableStorage" Name="GrainStore" 
        DataConnectionString="ServiceConfigurationSetting:GrainStoreDataConnectionString" />
    </StorageProviders>
  </Globals>
</OrleansConfiguration>
```

Note that you can still provided settings directly in the OrleansConfiguration, by just not prefixing them with "ServiceConfigurationSetting:".

And then add the GrainStoreDataConnectionString to the settings of the cloud service in Azures ServiceConfiguration.csdef:

```xml
<ServiceDefinition name="[...]" xmlns="http://schemas.microsoft.com/ServiceHosting/2008/10/ServiceDefinition" schemaVersion="2014-06.2.4">
  <WorkerRole name="[...]" vmsize="Small">
    <ConfigurationSettings>
      <Setting name="GrainStoreDataConnectionString" />
    </ConfigurationSettings>
    ...
  </WorkerRole>
</ServiceDefinition>
```

And the desired value to Azures ServiceConfiguration.cscfg:

```xml
<ServiceConfiguration serviceName="Imp.Azure.OrleansSilo" xmlns="http://schemas.microsoft.com/ServiceHosting/2008/10/ServiceConfiguration" osFamily="4" osVersion="*" schemaVersion="2014-06.2.4">
  <Role name="Imp.Azure.OrleansSilo">
    <Instances count="1" />
    <ConfigurationSettings>
      <Setting name="GrainStoreDataConnectionString" value="DefaultEndpointsProtocol=https;AccountName=[...];AccountKey=[...]" />
      ...
    </ConfigurationSettings>
  </Role>
  ...
</ServiceConfiguration>
```

