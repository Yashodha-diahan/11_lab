# Build stage: restore and publish a Release build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish CampusEats.Api.csproj -c Release -o /app

# Runtime stage: small ASP.NET Core image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
# Listen on all interfaces on the platform port
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "CampusEats.Api.dll"]
