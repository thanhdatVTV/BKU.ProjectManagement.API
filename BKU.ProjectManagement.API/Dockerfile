# Use official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["BKU.ProjectManagement.API/BKU.ProjectManagement.API.csproj", "BKU.ProjectManagement.API/"]
COPY ["BKU.ProjectManagement.Repositories/BKU.ProjectManagement.Repositories.csproj", "BKU.ProjectManagement.Repositories/"]
COPY ["BKU.ProjectManagement.Services/BKU.ProjectManagement.Services.csproj", "BKU.ProjectManagement.Services/"]
COPY ["BKU.ProjectManagement.Shared/BKU.ProjectManagement.Shared.csproj", "BKU.ProjectManagement.Shared/"]

RUN dotnet restore "BKU.ProjectManagement.API/BKU.ProjectManagement.API.csproj"

# Copy everything else and build the app
COPY . .
WORKDIR "/src/BKU.ProjectManagement.API"
RUN dotnet build "BKU.ProjectManagement.API.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "BKU.ProjectManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: Use runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BKU.ProjectManagement.API.dll"]
