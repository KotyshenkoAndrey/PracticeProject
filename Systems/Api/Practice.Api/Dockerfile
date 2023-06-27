#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/Practice.Api/Practice.Api.csproj", "Systems/Api/Practice.Api/"]
COPY ["Data/Migrations/Practice.Context.MigrationsMSSQL/Practice.Context.MigrationsMSSQL.csproj", "Data/Migrations/Practice.Context.MigrationsMSSQL/"]
COPY ["Data/Practice.Context.Entities/Practice.Context.Entities.csproj", "Data/Practice.Context.Entities/"]
COPY ["Data/Practice.Context/Practice.Context.csproj", "Data/Practice.Context/"]
COPY ["Shared/Practice.Settings/Practice.Settings.csproj", "Shared/Practice.Settings/"]
COPY ["Data/Migrations/Practice.Migrations.PostgreSQL/Practice.Context.MigrationsPostgreSQL.csproj", "Data/Migrations/Practice.Migrations.PostgreSQL/"]
COPY ["Service/Practice.Services.Books/Practice.Services.Books.csproj", "Service/Practice.Services.Books/"]
COPY ["Shared/Practice.Common/Practice.Common.csproj", "Shared/Practice.Common/"]
COPY ["Service/Practice.Services.Settings/Practice.Services.Settings.csproj", "Service/Practice.Services.Settings/"]
COPY ["Service/Practice.Services.UserAccount/Practice.Services.UserAccount.csproj", "Service/Practice.Services.UserAccount/"]
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