FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Копируем файл проекта, учитывая папку с пробелами
COPY ["on it 1/on it 1.csproj", "on it 1/"]
RUN dotnet restore "on it 1/on it 1.csproj"
COPY . .
WORKDIR "/src/on it 1"
RUN dotnet build "on it 1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "on it 1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "on it 1.dll"]