# Masked Emails Sample Data

This project is a simple console application that adds sample data to the "Masked Emails" CosmosDb database.

## Initializing

This project makes use of some configuration parameters. Most of which can be stored using the ASP.Net Secrets Manager:

- `TableStorage:ConnectionString`: connection string to an Azure Storage Account.

- `CosmosDb:EndpointUri`: URI to the CosmosDb service.
- `CosmosDb:PrimaryKey`: primary key to the CosmosDb service.
- `CosmosDb:IgnoreSslServerCertificateValidation`: set to `true` to ignore certificate validation, useful when running the CosmosDb emulator from a Docker container, for instance. Defaults to `false`.

The project comes with sample data that you must seed using the following command:

```
> cd /src/seed
> dotnet run -- /seed
```

This will create the initial CosmosDb database and container, and insert some sample records.
