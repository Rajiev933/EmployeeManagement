# Use the official .NET 9.0 SDK image for build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy solution and all project files
COPY EmployeeManagement.sln ./
COPY EmployeeManagement.API/EmployeeManagement.API.csproj EmployeeManagement.API/
COPY EmployeeManagement.Application/EmployeeManagement.Application.csproj EmployeeManagement.Application/
COPY EmployeeManagement.Infrastructure/EmployeeManagement.Infrastructure.csproj EmployeeManagement.Infrastructure/

# Restore dependencies for the entire solution
RUN dotnet restore EmployeeManagement.sln

# Copy the rest of the source code
COPY . .

# Build the solution in Release mode
RUN dotnet build EmployeeManagement.sln -c Release -o /app/build

# Publish the API project to a folder for deployment
RUN dotnet publish EmployeeManagement.API/EmployeeManagement.API.csproj -c Release -o /app/publish

# Use the official .NET 9.0 ASP.NET runtime image for runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/publish .

# Expose port if your app listens on a specific port
EXPOSE 5050

# Start the app
ENTRYPOINT ["dotnet", "EmployeeManagement.API.dll"]
