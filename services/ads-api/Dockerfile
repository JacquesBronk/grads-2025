FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/ads-api/Retro.Ads.csproj", "services/ads-api/"]
COPY ["services/common/Retro.Configuration/Retro.Configuration.csproj", "services/common/Retro.Configuration/"]
RUN dotnet restore "services/ads-api/Retro.Ads.csproj"
COPY . .
WORKDIR "/src/services/ads-api"
RUN dotnet build "Retro.Ads.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Retro.Ads.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retro.Ads.dll"]
