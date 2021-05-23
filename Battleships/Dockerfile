FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY Battleships.Api/bin/Release/net5.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "Battleships.Api.dll"]
