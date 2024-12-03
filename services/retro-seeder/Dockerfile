# Stage 1: Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#install curl
RUN apt-get update && apt-get install -y curl
# clean references
RUN rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and dependencies
COPY services/common/Retro.Http/ ./services/common/Retro.Http/
COPY services/retro-seeder/ ./services/retro-seeder/
COPY services/common/Retro.Persistence.Mongo/ ./services/common/Retro.Persistence.Mongo/
COPY services/libs/Retro.Stock.Domain/ ./services/libs/Retro.Stock.Domain/


# Copy seed data and error if not found
RUN mkdir -p ./tmp/seed/consul
COPY init/ ./tmp/seed/consul/
# Check if seed data is copied
RUN test -f ./tmp/seed/consul/yarp.json || (echo "Seed data not found" && exit 1)

# Restore dependencies
RUN dotnet restore "./services/retro-seeder/Retro.Seeder.csproj"

# Build the project
WORKDIR /src
RUN dotnet build "services/retro-seeder/Retro.Seeder.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "services/retro-seeder/Retro.Seeder.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/tmp/seed/consul /app/seed/consul
RUN test -f /app/seed/consul/yarp.json || (echo "Seed data not found" && exit 1)


ENTRYPOINT ["dotnet", "Retro.Seeder.dll"]
