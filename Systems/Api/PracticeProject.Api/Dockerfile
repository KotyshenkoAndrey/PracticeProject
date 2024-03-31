#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Api/PracticeProject.Api/PracticeProject.Api.csproj", "Systems/Api/PracticeProject.Api/"]
COPY ["Data/PracticeProject.Context/PracticeProject.Context.csproj", "Data/PracticeProject.Context/"]
COPY ["Services/PracticeProject.Service.Settings/PracticeProject.Service.Settings.csproj", "Services/PracticeProject.Service.Settings/"]
COPY ["Shared/PracticeProject.Common/PracticeProject.Common.csproj", "Shared/PracticeProject.Common/"]
COPY ["Data/PracticeProject.Context.Entities/PracticeProject.Context.Entities.csproj", "Data/PracticeProject.Context.Entities/"]
COPY ["Services/PracticeProject.Services.Logger/PracticeProject.Services.Logger.csproj", "Services/PracticeProject.Services.Logger/"]
RUN dotnet restore "./Systems/Api/PracticeProject.Api/PracticeProject.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/PracticeProject.Api"
RUN dotnet build "./PracticeProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PracticeProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PracticeProject.Api.dll"]