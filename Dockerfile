FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /
COPY ./ ./

RUN dotnet restore ./Sovos.Invoicing.sln

WORKDIR /src/Host/Sovos.Invoicing.Api

RUN dotnet build Sovos.Invoicing.Api.csproj -c Release
RUN dotnet publish Sovos.Invoicing.Api.csproj -c Release -o /app/published

WORKDIR /app/published

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime 

COPY --from=build /app/published .

ENTRYPOINT ["dotnet", "Sovos.Invoicing.Api.dll"]