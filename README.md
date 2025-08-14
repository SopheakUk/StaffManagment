set ASPNETCORE_ENVIRONMENT=Test
dotnet ef database update
dotnet tool install --global dotnet-ef

winget install --id Microsoft.PowerShell --source winget
pwsh bin/Debug/net9.0/playwright.ps1 install
