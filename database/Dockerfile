# Stage 1: Build DACPAC
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy and restore SQL Server project
COPY ["Product.Database/Product.Database.sqlproj", "./"]
RUN dotnet restore "Product.Database.sqlproj"

# Copy the entire solution and build
COPY . .
WORKDIR "/app/Product.Database"
RUN dotnet build /p:NetCoreBuild=true -c Release

# Stage 2: Deploy DACPAC to SQL Server
FROM mcr.microsoft.com/mssql/server:2019-latest AS deploy
ENV SA_PASSWORD="0000"
ENV ACCEPT_EULA="Y"
ENV MSSQL_DB="Product"
ENV MSSQL_USER="sa"
ENV MSSQL_PASSWORD="0000"
ENV MSSQL_PID="Express"

# Create a working directory
WORKDIR /deploy

# Copy built artifacts
COPY --from=build /app/Product.Database/bin/Release/ ./

# Install .NET SDK and Runtime for sqlpackage
USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       wget \
    && rm -rf /var/lib/apt/lists/* \
    && wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y --no-install-recommends dotnet-sdk-6.0 dotnet-runtime-6.0 \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Start SQL Server in the background
RUN /opt/mssql/bin/sqlservr & sleep 15

# Install sqlpackage
RUN dotnet tool install -g microsoft.sqlpackage --version 19.1.2

# Run the DACPAC deployment
RUN /root/.dotnet/tools/sqlpackage /a:Publish /tsn:product-database /tdn:Product /tu:sa /tp:0000 /sf:Product.Database.dacpac

# Stage 3: Final SQL Server image
FROM mcr.microsoft.com/mssql/server:2019-latest
ENV SA_PASSWORD=0000
ENV ACCEPT_EULA=Y
ENV MSSQL_DB=Product
ENV MSSQL_USER=sa
ENV MSSQL_PASSWORD=0000
ENV MSSQL_PID=Express

# Copy the deployed data from the deploy stage
COPY --from=deploy /deploy/Product.Database.dacpac /var/opt/mssql/data/

EXPOSE 1433