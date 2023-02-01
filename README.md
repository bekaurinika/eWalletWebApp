To use:
Run psql service

change or match -"EWalletConnection": "Server=localhost;Database=eWallet;Port=9090;User Id=postgres;Password=pgpassword"

To create database, open terminal on project root dir:
dotnet ef migrations add "InitialDatabase"
dotnet ef database update
 
