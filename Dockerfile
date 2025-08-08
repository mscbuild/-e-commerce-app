# Use the SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY MyWebApp.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Publish the app (Release mode, self-contained or framework-dependent)
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port (default ASP.NET Core port)
EXPOSE 80
EXPOSE 443

# Set the entry point
ENTRYPOINT ["dotnet", "MyWebApp.dll"]
