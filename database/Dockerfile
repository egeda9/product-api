# Stage 1: Install SQL Server and Run DACPAC
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ["Product.Database/Product.Database.sqlproj", "./"]
RUN dotnet restore "Product.Database.sqlproj"

COPY . .
WORKDIR "/app/Product.Database"
RUN dotnet build /p:NetCoreBuild=true -c Release

# Stage 2: Deploy DACPAC to SQL Server
FROM mcr.microsoft.com/mssql/server:2019-latest AS deploy
ENV SA_PASSWORD=0000
ENV ACCEPT_EULA=Y
ENV MSSQL_DB=Product
ENV MSSQL_USER=sa
ENV MSSQL_PASSWORD=0000
ENV MSSQL_PID=Express

COPY --from=build /app/Product.Database/bin/Release/ ./

# Set the .NET Core runtime version to 6.0.0
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH=$PATH:/usr/share/dotnet

# Install .NET SDK and Runtime
USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       wget \
    && rm -rf /var/lib/apt/lists/* \
    && wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y --no-install-recommends dotnet-sdk-7.0 dotnet-runtime-7.0 \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Set the .NET Core runtime version to the latest
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH=$PATH:/usr/share/dotnet

# Run the DACPAC file
RUN dotnet tool install -g Microsoft.SqlServer.DacFx --version 17.4.1 \
    && dotnet tool install -g microsoft.sqlpackage --version 19.1.2

# Set proper permissions
RUN chmod -R 777 /usr/share/dotnet/tools
RUN chmod -R 777 /root/.dotnet

# Stage 3: Final SQL Server image
FROM mcr.microsoft.com/mssql/server:2019-latest
ENV SA_PASSWORD=0000
ENV ACCEPT_EULA=Y
ENV MSSQL_DB=Product
ENV MSSQL_USER=sa
ENV MSSQL_PASSWORD=0000
ENV MSSQL_PID=Express

COPY --from=deploy /var/opt/mssql/data/ /var/opt/mssql/data/

EXPOSE 1433