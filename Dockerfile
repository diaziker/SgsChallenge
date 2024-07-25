FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ENV ASPNETCORE_HTTP_PORTS=80
#ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app

COPY src/SGS.Api/SGS.Api.csproj src/SGS.Api/
COPY src/SGS.Infrastructure/SGS.Infrastructure.csproj src/SGS.Infrastructure/
COPY src/SGS.Domain/SGS.Domain.csproj src/SGS.Domain/

WORKDIR /app/src/SGS.Api
RUN dotnet restore

COPY src/SGS.Api/ ./
COPY src/SGS.Infrastructure/ ../SGS.Infrastructure/
COPY src/SGS.Domain/ ../SGS.Domain/

RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SGS.Api.dll"]
