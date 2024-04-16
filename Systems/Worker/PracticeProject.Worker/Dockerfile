#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Worker/PracticeProject.Worker/PracticeProject.Worker.csproj", "Systems/Worker/PracticeProject.Worker/"]
COPY ["Services/Practice.Services.Actions/PracticeProject.Services.Actions.csproj", "Services/Practice.Services.Actions/"]
COPY ["Services/Practice.Services.RabbitMq/PracticeProject.Services.RabbitMq.csproj", "Services/Practice.Services.RabbitMq/"]
COPY ["Services/PracticeProject.Service.Settings/PracticeProject.Service.Settings.csproj", "Services/PracticeProject.Service.Settings/"]
COPY ["Shared/PracticeProject.Common/PracticeProject.Common.csproj", "Shared/PracticeProject.Common/"]
COPY ["Services/PracticeProject.Services.Logger/PracticeProject.Services.Logger.csproj", "Services/PracticeProject.Services.Logger/"]
RUN dotnet restore "./Systems/Worker/PracticeProject.Worker/PracticeProject.Worker.csproj"
COPY . .
WORKDIR "/src/Systems/Worker/PracticeProject.Worker"
RUN dotnet build "./PracticeProject.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PracticeProject.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PracticeProject.Worker.dll"]