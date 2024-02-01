FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SearchLocationApi/SearchLocationApi.csproj", "SearchLocationApi/"]
RUN dotnet restore "SearchLocationApi/SearchLocationApi.csproj"
COPY . .
WORKDIR "/src/SearchLocationApi"
RUN dotnet build "SearchLocationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SearchLocationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchLocationApi.dll"]
