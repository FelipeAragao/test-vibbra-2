#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Glauber.NotificationSystem.Api/Glauber.NotificationSystem.Api.csproj", "src/Glauber.NotificationSystem.Api/"]
RUN dotnet restore "src/Glauber.NotificationSystem.Api/Glauber.NotificationSystem.Api.csproj"
COPY . .
WORKDIR "/src/src/Glauber.NotificationSystem.Api"
RUN dotnet build "Glauber.NotificationSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Glauber.NotificationSystem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Glauber.NotificationSystem.Api.dll"]