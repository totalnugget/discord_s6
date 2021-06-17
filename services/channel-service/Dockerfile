ARG SERVICE="./services/channel-service"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dev
# Reuse build-arg
ARG SERVICE

WORKDIR /app

COPY $SERVICE/../../nuget.config .
COPY $SERVICE/Channel-Service.Web/*.csproj ./Channel-Service.Web/
RUN dotnet restore ./Channel-Service.Web

# Copy everything else and build
COPY $SERVICE/ .  
CMD [ "dotnet", "watch", "--project", "/app/Channel-Service.Web/Channel-Service.Web.csproj", "run"]

FROM dev as builder
RUN dotnet publish Channel-Service.Web/*.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=builder /app/out .
ENTRYPOINT ["dotnet", "Channel-Service.Web.dll"]
