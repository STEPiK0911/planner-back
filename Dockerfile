FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RoutePlannerApi/RoutePlannerApi.csproj", "RoutePlannerApi/"]
RUN dotnet restore "RoutePlannerApi/RoutePlannerApi.csproj"
COPY . .
WORKDIR "/src/RoutePlannerApi"
RUN dotnet build "RoutePlannerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RoutePlannerApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoutePlannerApi.dll"]
