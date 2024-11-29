FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/cart-api/Retro.Cart.csproj", "services/cart-api/"]
COPY ["services/common/Retro.Configuration/Retro.Configuration.csproj", "services/common/Retro.Configuration/"]
RUN dotnet restore "services/cart-api/Retro.Cart.csproj"
COPY . .
WORKDIR "/src/services/cart-api"
RUN dotnet build "Retro.Cart.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Retro.Cart.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retro.Cart.dll"]
