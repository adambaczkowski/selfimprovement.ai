FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY IdentityApi.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM base as final
COPY --from=build /app .
ENTRYPOINT ["dotnet", "IdentityApi.dll"]
