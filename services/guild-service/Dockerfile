ARG SERVICE="./services/guild-service"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dev
# Reuse build-arg
ARG SERVICE

WORKDIR /app

COPY $SERVICE/../../nuget.config .
COPY $SERVICE/Guild-Service.Web/*.csproj ./Guild-Service.Web/
RUN dotnet restore ./Guild-Service.Web

# Copy everything else and build
COPY $SERVICE/ .  
CMD [ "dotnet", "watch", "--project", "/app/Guild-Service.Web/Guild-Service.Web.csproj", "run"]

FROM dev as builder
RUN dotnet publish Guild-Service.Web/*.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=builder /app/out .
ENTRYPOINT ["dotnet", "Guild-Service.Web.dll"]
