ARG SERVICE="./services/user-service"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dev
# Reuse build-arg
ARG SERVICE

WORKDIR /app

COPY $SERVICE/../../nuget.config .
COPY $SERVICE/User-Service.Web/*.csproj ./User-Service.Web/
RUN dotnet restore ./User-Service.Web

# Copy everything else and build
COPY $SERVICE/ .  
CMD [ "dotnet", "watch", "--project", "/app/User-Service.Web/User-Service.Web.csproj", "run"]

FROM dev as builder
RUN dotnet publish User-Service.Web/*.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=builder /app/out .
ENTRYPOINT ["dotnet", "User-Service.Web.dll"]
