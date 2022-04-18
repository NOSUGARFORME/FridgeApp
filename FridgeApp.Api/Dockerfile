FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FridgeApp.Api/FridgeApp.Api.csproj", "FridgeApp.Api/"]
COPY ["FridgeApp.Infrastructure/FridgeApp.Infrastructure.csproj", "FridgeApp.Infrastructure/"]
COPY ["FridgeApp.Application/FridgeApp.Application.csproj", "FridgeApp.Application/"]
COPY ["FridgeApp.Domain/FridgeApp.Domain.csproj", "FridgeApp.Domain/"]
COPY ["FridgeApp.Shared.Abstractions/FridgeApp.Shared.Abstractions.csproj", "FridgeApp.Shared.Abstractions/"]
COPY ["FridgeApp.Shared/FridgeApp.Shared.csproj", "FridgeApp.Shared/"]
RUN dotnet restore "FridgeApp.Api/FridgeApp.Api.csproj"
COPY . .
WORKDIR "/src/FridgeApp.Api"
RUN dotnet build "FridgeApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FridgeApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FridgeApp.Api.dll"]
