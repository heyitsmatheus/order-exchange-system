FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

WORKDIR /src

COPY src/OrderApi/OrderApi.csproj src/OrderApi/
RUN dotnet restore src/OrderApi/OrderApi.csproj

COPY src/OrderApi/. src/OrderApi/

RUN dotnet publish src/OrderApi/OrderApi.csproj \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:10.0-preview
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrderApi.dll"]



