For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c_%%a_%%b)
For /f "tokens=1-2 delims=/:" %%a in ("%TIME: =0%") do (set mytime=%%a%%b)
dotnet tool update --global dotnet-ef --version 7.0.2
dotnet tool restore
dotnet build
dotnet ef migrations --startup-project ../ASPNETCoreIdentitySample/ add V%mydate%_%mytime% --context MsSqlDbContext
pause