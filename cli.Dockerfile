# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY src ./src/ /app/

WORKDIR /app/Tlis.Cms.Cli/src

RUN dotnet publish -c Release -o out

# Build Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/Tlis.Cms.Cli/src/out .

ENTRYPOINT [ "dotnet", "Tlis.Cms.Cli.dll" ]