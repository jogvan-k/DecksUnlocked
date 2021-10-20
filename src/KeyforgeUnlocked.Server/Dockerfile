FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["KeyforgeUnlocked.Server/KeyforgeUnlocked.Server.csproj", "KeyforgeUnlocked.Server/"]
RUN dotnet restore "KeyforgeUnlocked.Server/KeyforgeUnlocked.Server.csproj"
COPY . .
WORKDIR "/src/KeyforgeUnlocked.Server"
RUN dotnet build "KeyforgeUnlocked.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KeyforgeUnlocked.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeyforgeUnlocked.Server.dll"]
