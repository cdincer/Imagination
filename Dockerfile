# Feel free to replace this with a more appropriate Dockerfile.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Imagination.Server/Imagination.Server.csproj", "src/Imagination.Server/"]
RUN dotnet restore "src/Imagination.Server/Imagination.Server.csproj"
COPY . .
WORKDIR "/src/src/Imagination.Server"
RUN dotnet build "Imagination.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Imagination.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imagination.Server.dll"]

FROM alpine
RUN apk update
CMD ["apk", "fetch", "coffee"]
