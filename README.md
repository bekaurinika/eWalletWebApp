To use:

Run psql service

Update or match appsettings.json/"EWalletConnection": "Server=localhost;Database=eWallet;Port=5432;User Id=postgres;Password=pgpassword"

To create database, open terminal on project root dir:

dotnet ef migrations add "InitialDatabase"

dotnet ef database update
 
