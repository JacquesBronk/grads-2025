FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/vendors-api/Retro.Vendors/Retro.Vendors.csproj", "services/vendors-api/Retro.Vendors/"]
RUN dotnet restore "services/vendors-api/Retro.Vendors/Retro.Vendors.csproj"
COPY . .
WORKDIR "/src/services/vendors-api/Retro.Vendors"
RUN dotnet build "Retro.Vendors.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Retro.Vendors.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retro.Vendors.dll"]
