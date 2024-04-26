#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Systems/Web/PracticeProject.Web/PracticeProject.Web.csproj", "Systems/Web/PracticeProject.Web/PracticeProject.Web.csproj"]
RUN dotnet restore "Systems/Web/PracticeProject.Web/PracticeProject.Web.csproj"
COPY . .
WORKDIR "/src/Systems/Web/PracticeProject.Web"
RUN dotnet build "PracticeProject.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PracticeProject.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY Systems/Web/PracticeProject.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 8080