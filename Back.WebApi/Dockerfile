# ====================
# 1) Build stage
# ====================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем только csproj-ы для кэширования слоёв
COPY ["Back.Domain/Back.Domain.csproj",       "Back.Domain/"]
COPY ["Back.Application/Back.Application.csproj","Back.Application/"]
COPY ["Back.Infrastructure/Back.Infrastructure.csproj","Back.Infrastructure/"]
COPY ["Back.WebApi/Back.WebApi.csproj",       "Back.WebApi/"]

# Восстанавливаем зависимости
RUN dotnet restore "Back.WebApi/Back.WebApi.csproj"

# Копируем остальное решение
COPY . .

# Публикуем в папку /app/publish
WORKDIR "/src/Back.WebApi"
RUN dotnet publish -c Release -o /app/publish

# ====================
# 2) Runtime stage
# ====================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Копируем готовые артефакты
COPY --from=build /app/publish .

# Создаём папку для загружаемых фото
RUN mkdir -p /app/Uploads

# Объявляем точку монтирования (вы потом при docker run сможете монтировать в неё внешний каталог)
VOLUME [ "/app/Uploads" ]

# Открываем порты (если в launchSettings они 5144/7188 — прокиньте их при docker run)
EXPOSE 5144
EXPOSE 7188

# Точка входа
ENTRYPOINT ["dotnet", "Back.WebApi.dll"]
