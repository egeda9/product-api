# Stage 1: Build the .NET Core application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ["Product.Api/Product.Api.csproj", "./"]
RUN dotnet restore "Product.Api.csproj"

COPY . .
WORKDIR "/app"
RUN dotnet build "Product.Api/Product.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Product.Api/Product.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./

EXPOSE 80

ENV ConnectionStrings__DefaultConnection="Server=sql-server,1433;Database=Product;User=sa;Password=0000;"

ENTRYPOINT ["dotnet", "Product.Api.dll"]