# Use .NET SDK to build and publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY MyWebApp.csproj ./
RUN dotnet restore

# Copy all source code
COPY . .

# Publish the app
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80 (HTTP)
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "MyWebApp.dll"]
