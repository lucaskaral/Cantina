Instalar Visual Studio 2022

Instalar migrations
dotnet tool install --global dotnet-ef

Alterar appsettings.json com os dados corretos da base de dados

Executar o comando 
dotnet ef database update

Abrir o projeto no visual studio e executar (caso o visual studio pe�a para instalar algum componente, permitir a instala��o).



--Evolu��o das classes e base de dados (N�o executar, apenas hist�rico)

dotnet ef database update

dotnet ef migrations add Identity
dotnet ef database update
dotnet ef migrations add OrderProduct
dotnet ef database update
dotnet ef migrations add ProductsOrders
dotnet ef migrations add NewColumnsOnOrderProduct
dotnet ef migrations add Ajuste
dotnet ef database update