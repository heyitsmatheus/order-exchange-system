FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

COPY src/OrderExecutionWorker/OrderExecutionWorker.csproj src/OrderExecutionWorker/
RUN dotnet restore src/OrderExecutionWorker/OrderExecutionWorker.csproj

COPY src/OrderExecutionWorker/. src/OrderExecutionWorker/

RUN dotnet publish src/OrderExecutionWorker/OrderExecutionWorker.csproj \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:10.0-preview
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrderExecutionWorker.dll"]

