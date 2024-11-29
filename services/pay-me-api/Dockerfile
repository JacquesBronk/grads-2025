FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/pay-me-api/Retro.Payments.csproj", "services/pay-me-api/"]
COPY ["services/common/Retro.Configuration/Retro.Configuration.csproj", "services/common/Retro.Configuration/"]
RUN dotnet restore "services/pay-me-api/Retro.Payments.csproj"
COPY . .
WORKDIR "/src/services/pay-me-api"
RUN dotnet build "Retro.Payments.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Retro.Payments.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retro.Payments.dll"]
