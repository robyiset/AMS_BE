#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ASM_Service_Notification/ASM_Service_Notification.csproj", "ASM_Service_Notification/"]
RUN dotnet restore "ASM_Service_Notification/ASM_Service_Notification.csproj"
COPY . .
WORKDIR "/src/ASM_Service_Notification"
RUN dotnet build "ASM_Service_Notification.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASM_Service_Notification.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASM_Service_Notification.dll"]