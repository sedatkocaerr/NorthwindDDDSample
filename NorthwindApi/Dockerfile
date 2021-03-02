#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NorthwindApi/NorthwindApi.csproj", "NorthwindApi/"]
COPY ["NorthwindApi.CrossCutting.IoC/NorthwindApi.CrossCutting.IoC.csproj", "NorthwindApi.CrossCutting.IoC/"]
COPY ["NorthwindApi.Application/NorthwindApi.Application.csproj", "NorthwindApi.Application/"]
COPY ["NorthwindApi.Data/NorthwindApi.Data.csproj", "NorthwindApi.Data/"]
COPY ["NorthwindApi.Domain/NorthwindApi.Domain.csproj", "NorthwindApi.Domain/"]
COPY ["NorthwindApi.Domain.Core/NorthwindApi.Domain.Core.csproj", "NorthwindApi.Domain.Core/"]
RUN dotnet restore "NorthwindApi/NorthwindApi.csproj"
COPY . .
WORKDIR "/src/NorthwindApi"
RUN dotnet build "NorthwindApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NorthwindApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NorthwindApi.dll"]