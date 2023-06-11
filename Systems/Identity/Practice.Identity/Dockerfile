#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Identity/Practice.Identity/Practice.Identity.csproj", "Systems/Identity/Practice.Identity/"]
COPY ["Data/Practice.Context.Entities/Practice.Context.Entities.csproj", "Data/Practice.Context.Entities/"]
COPY ["Data/Practice.Context/Practice.Context.csproj", "Data/Practice.Context/"]
COPY ["Shared/Practice.Settings/Practice.Settings.csproj", "Shared/Practice.Settings/"]
COPY ["Shared/Practice.Common/Practice.Common.csproj", "Shared/Practice.Common/"]
RUN dotnet restore "Systems/Identity/Practice.Identity/Practice.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/Practice.Identity"
RUN dotnet build "Practice.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Practice.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Practice.Identity.dll"]