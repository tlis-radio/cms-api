# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY src ./src/Tlis.Cms/ /App/

WORKDIR /App/Api/src

RUN dotnet publish -c Release -o out

# Build Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

COPY --from=build-env /App/Api/src/out .

ENTRYPOINT ["dotnet", "Tlis.Cms.Api.dll"]