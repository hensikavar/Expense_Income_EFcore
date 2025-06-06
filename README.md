package to be dowmload
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

CLI:
dotnet ef migrations add InitialCreate
dotnet ef database update

Tools → NuGet Package Manager → Package Manager Console (PMC):
Add-Migration InitialCreate
Update-Database


![{5CCA8CFE-2B78-45C8-91DC-B796DA5519EE}](https://github.com/user-attachments/assets/8f7d5847-c82b-4532-a034-0132d4b48fd6)




