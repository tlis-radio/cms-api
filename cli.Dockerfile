# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY src ./src/ /App/

WORKDIR /App/Tlis.Cms.Cli/src

RUN dotnet publish -c Release -o out

# Build Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

COPY --from=build-env /App/Tlis.Cms.Cli/src/out .

ENTRYPOINT [ "dotnet", "Tlis.Cms.Cli.dll" ]