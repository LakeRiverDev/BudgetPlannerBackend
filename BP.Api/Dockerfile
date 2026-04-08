FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5086
EXPOSE 7013

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BP.Api/BP.Api.csproj", "BP.Api/"]
COPY ["BP.Application/BP.Application.csproj", "BP.Application/"]
COPY ["BP.Contracts/BP.Contracts.csproj", "BP.Contracts/"]
COPY ["BP.Core/BP.Core.csproj", "BP.Core/"]
COPY ["BP.Infrastructure/BP.Infrastructure.csproj", "BP.Infrastructure/"]
RUN dotnet restore "BP.Api/BP.Api.csproj"
COPY . .
WORKDIR "/src/BP.Api"
RUN dotnet build "./BP.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BP.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BP.Api.dll"]
