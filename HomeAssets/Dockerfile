FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["HomeAssets/HomeAssets.csproj", "HomeAssets/"]
RUN dotnet restore -r linux-x64 "HomeAssets/HomeAssets.csproj"
COPY . .
WORKDIR /src/HomeAssets
RUN dotnet build "HomeAssets.csproj" -c Release -r linux-x64 -o /app/build

FROM build AS publish
RUN dotnet publish "HomeAssets.csproj" -c Release -r linux-x64 --self-contained false --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeAssets.dll"]