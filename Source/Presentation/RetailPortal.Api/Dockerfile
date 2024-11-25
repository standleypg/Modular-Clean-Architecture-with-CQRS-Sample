# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["Presentation/RetailPortal.Api/RetailPortal.Api.csproj", "Presentation/RetailPortal.Api/"]
COPY ["Core/RetailPortal.Core/RetailPortal.Core.csproj", "Core/RetailPortal.Core/"]
COPY ["Core/RetailPortal.Application/RetailPortal.Application.csproj", "Core/RetailPortal.Application/"]
COPY ["Core/RetailPortal.Domain/RetailPortal.Domain.csproj", "Core/RetailPortal.Domain/"]
COPY ["Shared/RetailPortal.Shared/RetailPortal.Shared.csproj", "Shared/RetailPortal.Shared/"]
COPY ["Infrastructure/RetailPortal.Infrastructure/RetailPortal.Infrastructure.csproj", "Infrastructure/RetailPortal.Infrastructure/"]
RUN dotnet restore "./Presentation/RetailPortal.Api/RetailPortal.Api.csproj"
COPY . .
WORKDIR "/src/Presentation/RetailPortal.Api"
RUN dotnet build "./RetailPortal.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RetailPortal.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RetailPortal.Api.dll"]