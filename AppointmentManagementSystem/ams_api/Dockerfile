#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AppointmentManagementSystem/ams_api/ams_api.csproj", "AppointmentManagementSystem/ams_api/"]
RUN dotnet restore "AppointmentManagementSystem/ams_api/ams_api.csproj"
COPY . .
WORKDIR "/src/AppointmentManagementSystem/ams_api"
RUN dotnet build "ams_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ams_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ams_api.dll"]