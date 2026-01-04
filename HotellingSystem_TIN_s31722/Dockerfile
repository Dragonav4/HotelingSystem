FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HotellingSystem_TIN_s31722/HotellingSystem_TIN_s31722.csproj", "HotellingSystem_TIN_s31722/"]
RUN dotnet restore "HotellingSystem_TIN_s31722/HotellingSystem_TIN_s31722.csproj"
COPY . .
WORKDIR "/src/HotellingSystem_TIN_s31722"
RUN dotnet build "HotellingSystem_TIN_s31722.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HotellingSystem_TIN_s31722.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotellingSystem_TIN_s31722.dll"]
