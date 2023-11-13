FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY . ./app
WORKDIR ./app/src/AdvertisementsBoard/Hosts/AdvertisementsBoard.Hosts.Migrator
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/AdvertisementsBoard/Hosts/AdvertisementsBoard.Hosts.Migrator/out ./
ENTRYPOINT ["dotnet", "AdvertisementsBoard.Hosts.Migrator.dll"]

ADD https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh