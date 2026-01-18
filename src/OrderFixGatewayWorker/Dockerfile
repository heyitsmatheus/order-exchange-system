FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

COPY src/OrderFixGatewayWorker/OrderFixGatewayWorker.csproj src/OrderFixGatewayWorker/
RUN dotnet restore src/OrderFixGatewayWorker/OrderFixGatewayWorker.csproj

COPY src/OrderFixGatewayWorker/. src/OrderFixGatewayWorker/

RUN dotnet publish src/OrderFixGatewayWorker/OrderFixGatewayWorker.csproj \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:10.0-preview
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrderFixGatewayWorker.dll"]
