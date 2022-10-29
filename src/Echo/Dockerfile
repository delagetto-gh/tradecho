FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app/src
COPY . .
RUN dotnet build src/Echo/App/App.csproj -c Release
RUN dotnet publish src/Echo/App/App.csproj -o /app/dist

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
EXPOSE 80
WORKDIR /app
COPY --from=build /app/dist .
CMD [ "dotnet", "App.dll" ]