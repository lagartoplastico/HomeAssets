FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY HomeAssets/*.csproj ./HomeAssets/
RUN dotnet restore -r linux-x64

# copy everything else and build app
COPY HomeAssets/. ./HomeAssets/
WORKDIR /source/HomeAssets
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./HomeAssets"]