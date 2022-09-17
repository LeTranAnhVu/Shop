FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shop.Mvc/Shop.Mvc.csproj", "Shop.Mvc/"]
RUN dotnet restore "Shop.Mvc/Shop.Mvc.csproj"
COPY . .
WORKDIR "/src/Shop.Mvc"
RUN dotnet build "Shop.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Mvc.dll"]
