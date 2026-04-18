# Стейдж 1: Сборка
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл проекта (используем JSON-синтаксис для надежности)
COPY ["on it 1.csproj", "./"]
RUN dotnet restore "on it 1.csproj"

# Копируем всё остальное
COPY . .

# Сборка. ВНИМАНИЕ: убрал --no-restore, чтобы он еще раз всё проверил
RUN dotnet publish "on it 1.csproj" -c Release -o /app/publish

# Стейдж 2: Запуск
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Установка curl для healthcheck
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Запуск. Обязательно в кавычках!
ENTRYPOINT ["dotnet", "on it 1.dll"]