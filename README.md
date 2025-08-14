Description
This is Staff Management fullstack website backend and frontend

Requirement

- Install MariaDB 10.4.32
- Install .Net Core 9.0 => https://dotnet.microsoft.com/download/dotnet
- install ef core tool, go to command line and type
	+ dotnet tool install --global dotnet-ef

Deploy Backend api => go to command line under StaffManagementAPI folder and type
- dotnet build
- dotnet ef database update
- dotnet run bin\Debuy\net9.0\StaffManagementAPI.dll

Deploy Frontend => go to command line StaffManagement folder and type
- dotnet build
- dotnet run bin\Debuy\net9.0\StaffManagement.dll

Unit Test => go to command line under UnitTest folder and type
- dotnet test

Integration Test 
- go to command line under StaffManagementAPI folder and type
	+ set ASPNETCORE_ENVIRONMENT=Test
	+ dotnet ef database update
- go to command line under IntegrationTest folder and type
	+ dotnet test

End to End Test (Make sure backend and frontend are running) 
- Install Requirement
	+ winget install --id Microsoft.PowerShell --source winget
- Go to command line under EndToEndTest folder and type
	+ dotnet build
	+ pwsh bin/Debug/net9.0/playwright.ps1 install
	+ dotnet test