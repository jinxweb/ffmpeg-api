#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install FFmpeg in the container
RUN apt-get update && \
    apt-get install -y ffmpeg && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ffmpeg-api/ffmpeg-api.csproj", "ffmpeg-api/"]
RUN dotnet restore "ffmpeg-api/ffmpeg-api.csproj"
COPY . .
WORKDIR "/src/ffmpeg-api"
RUN dotnet build "ffmpeg-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ffmpeg-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ffmpeg-api.dll"]