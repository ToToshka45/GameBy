FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=5001 
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY RatingService.sln .
COPY RatingService.API/RatingService.API.csproj RatingService.API/
COPY RatingService.Application/RatingService.Application.csproj RatingService.Application/
COPY RatingService.Infrastructure/RatingService.Infrastructure.csproj RatingService.Infrastructure/
COPY RatingService.Domain/RatingService.Domain.csproj RatingService.Domain/
COPY RatingService.Common/RatingService.Common.csproj RatingService.Common/
RUN dotnet restore "RatingService.sln"

COPY . .
RUN dotnet build "RatingService.sln" --no-restore -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "RatingService.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RatingService.API.dll"]