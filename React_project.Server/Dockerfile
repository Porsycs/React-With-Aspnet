# Use the ASP.NET Core runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj file and restore dependencies
COPY ["./React_project.Server.csproj", "/"]
RUN dotnet restore "/React_project.Server.csproj"

# Copy the rest of the files and build the application
COPY . .
WORKDIR "./"
RUN dotnet build "React_project.Server.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "React_project.Server.csproj" -c Release -o /app/publish

# Set the final image with the published application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "React_project.Server.dll"]
