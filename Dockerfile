# Use the official .NET SDK image as a base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project files to the container
COPY . .

# Build the application
RUN dotnet build -c Release -o /app/out

# Use a lighter image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the built application from the build image
COPY --from=build /app/out .

# Expose the port the app will run on
EXPOSE 80
EXPOSE 54585

# Define the command to run the application
CMD ["dotnet", "SeminarAPI.dll"]
