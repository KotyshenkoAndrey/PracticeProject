#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/Practice.Api/Practice.Api.csproj", "Systems/Api/Practice.Api/"]
COPY ["Service/Practice.Services.Settings/Practice.Services.Settings.csproj", "Service/Practice.Services.Settings/"]
COPY ["Shared/Practice.Settings/Practice.Settings.csproj", "Shared/Practice.Settings/"]
COPY ["Shared/Practice.Common/Practice.Common.csproj", "Shared/Practice.Common/"]
COPY ["Systems/Api/Practice.Api.Settings/Practice.Api.Settings.csproj", "Systems/Api/Practice.Api.Settings/"]
RUN dotnet restore "Systems/Api/Practice.Api/Practice.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/Practice.Api"
RUN dotnet build "Practice.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Practice.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Practice.Api.dll"]