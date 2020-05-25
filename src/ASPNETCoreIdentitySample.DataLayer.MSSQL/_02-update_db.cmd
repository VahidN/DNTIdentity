dotnet tool install --global dotnet-ef --version 3.1.4
dotnet tool update --global dotnet-ef --version 3.1.4
dotnet build
dotnet ef --startup-project ../ASPNETCoreIdentitySample/ database update --context MsSqlDbContext
pause