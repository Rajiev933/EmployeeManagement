# Use the official ASP.NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy and restore dependencies
COPY ["EmployeeManagement.API/EmployeeManagement.API.csproj", "EmployeeManagement.API/"]
RUN dotnet restore "EmployeeManagement.API/EmployeeManagement.API.csproj"

# Copy the rest of the files and build
COPY . .
WORKDIR "/src/EmployeeManagement.API"
RUN dotnet build "EmployeeManagement.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "EmployeeManagement.API.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeManagement.API.dll"]
