Instalar Visual Studio 2022

Instalar migrations
dotnet tool install --global dotnet-ef

Alterar appsettings.json com os dados corretos da base de dados

Executar o comando 
dotnet ef database update

Abrir o projeto no visual studio e executar (caso o visual studio peça para instalar algum componente, permitir a instalação).



--Evolução das classes e base de dados (Não executar, apenas histórico)

dotnet ef database update

dotnet ef migrations add Cantina
dotnet ef migrations add AddColumnPriceOnOrder
dotnet ef database update