#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Garage-Finder-Backend/Garage-Finder-Backend.csproj", "Garage-Finder-Backend/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["GFData/GFData.csproj", "GFData/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "Garage-Finder-Backend/Garage-Finder-Backend.csproj"
COPY . .
WORKDIR "/src/Garage-Finder-Backend"
RUN dotnet build "Garage-Finder-Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Garage-Finder-Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Garage-Finder-Backend.dll"]