FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GoalApi/GoalApi.csproj", "GoalApi/"]
RUN dotnet restore "GoalApi/GoalApi.csproj"
COPY . .
WORKDIR "/src/GoalApi"
RUN dotnet build "GoalApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoalApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoalApi.dll"]
