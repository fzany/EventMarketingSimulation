FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EventCampaignManagement/EventCampaignManagement.csproj", "EventCampaignManagement/"]
RUN dotnet restore "EventCampaignManagement/EventCampaignManagement.csproj"
COPY . .
WORKDIR "/src/EventCampaignManagement"
RUN dotnet build "EventCampaignManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventCampaignManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventCampaignManagement.dll"]
