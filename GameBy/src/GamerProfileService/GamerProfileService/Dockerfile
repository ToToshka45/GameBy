# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
#COPY ["GamerProfileService/GamerProfileService.csproj", "Build/GamerProfileService/"]
#COPY ["Domain/Domain.Entities/Domain.Entities.csproj", "Build/Domain.Entities/"]
#COPY ["Infrastructure/Infrastructure.EntityFramework/Infrastructure.EntityFramework.csproj", "Build/Infrastructure.EntityFramework/"]
#COPY ["Infrastructure/Infrastructure.Repositories.Implementations/Infrastructure.Repositories.Implementations.csproj", "Build/Infrastructure.Repositories.Implementations/"]
#COPY ["Services/Services.Repositories.Abstractions/Services.Repositories.Abstractions.csproj", "Build/Services.Repositories.Abstractions/"]
#COPY ["Services/Services.Abstractions/Services.Abstractions.csproj", "Build/Services.Abstractions/"]
#COPY ["Services/Services.Implementations/Services.Implementations.csproj", "Build/Services.Implementations/"]
#COPY ["Services/Services.Contracts/Services.Contracts.csproj", "Build/Services.Contracts/"]
COPY . ./Build
RUN dotnet restore "./Build/GamerProfileService/GamerProfileService.csproj"
#COPY . .
WORKDIR "/src/Build/GamerProfileService"
RUN dotnet build "./GamerProfileService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GamerProfileService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#COPY --from=base /app/GameBy.DataAccess/LocalDatabase.db .
ENTRYPOINT ["dotnet", "GamerProfileService.dll"]
