# === Stage 1: build ===
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Back.Domain/Back.Domain.csproj",         "Back.Domain/"]
COPY ["Back.Application/Back.Application.csproj","Back.Application/"]
COPY ["Back.Infrastructure/Back.Infrastructure.csproj","Back.Infrastructure/"]
COPY ["Back.WebApi/Back.WebApi.csproj",         "Back.WebApi/"]
RUN dotnet restore "Back.WebApi/Back.WebApi.csproj"

COPY . .
WORKDIR "/src/Back.WebApi"
RUN dotnet publish -c Release -o /app/publish

# === Stage 2: runtime ===
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# копируем скомпилированные артефакты и сертификат
COPY --from=build /app/publish ./
COPY Back.WebApi/localhost.pfx /https/localhost.pfx

# папка для загрузок
RUN mkdir -p /app/Uploads
VOLUME [ "/app/Uploads" ]

# слушаем HTTP и HTTPS(7102)
ENV ASPNETCORE_URLS="http://+:5144;https://+:7102"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/localhost.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=MyPfxPassword

EXPOSE 5144 7102

ENTRYPOINT ["dotnet", "Back.WebApi.dll"]
