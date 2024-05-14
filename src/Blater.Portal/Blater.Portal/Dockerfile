FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Blater.Portal/Blater.Portal/Blater.Portal.csproj", "Blater.Portal/Blater.Portal/"]
COPY ["Blater.Portal/Blater.Portal.Client/Blater.Portal.Client.csproj", "Blater.Portal/Blater.Portal.Client/"]
RUN dotnet restore "Blater.Portal/Blater.Portal/Blater.Portal.csproj"
COPY . .
WORKDIR "/src/Blater.Portal/Blater.Portal"
RUN dotnet build "Blater.Portal.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Blater.Portal.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blater.Portal.dll"]
