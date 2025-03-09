FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TransactionsApp/TransactionsApp.csproj", "TransactionsApp/"]
RUN dotnet restore "TransactionsApp/TransactionsApp.csproj"
COPY . .
WORKDIR "/src/TransactionsApp"
RUN dotnet build "TransactionsApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TransactionsApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionsApp.dll", "--urls", "http://0.0.0.0:80"]
