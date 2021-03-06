FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Navtrack.Api/Navtrack.Api.csproj", "Navtrack.Api/"]
COPY ["Navtrack.Common/Navtrack.Common.csproj", "Navtrack.Common/"]
COPY ["Navtrack.Core/Navtrack.Core.csproj", "Navtrack.Core/"]
COPY ["Navtrack.DataAccess/Navtrack.DataAccess.csproj", "Navtrack.DataAccess/"]
COPY ["Navtrack.Library/Navtrack.Library.csproj", "Navtrack.Library/"]
COPY ["Navtrack.Listener/Navtrack.Listener.csproj", "Navtrack.Listener/"]
RUN dotnet restore "Navtrack.Core/Navtrack.Core.csproj"
COPY . .
WORKDIR /src/Navtrack.Core
RUN dotnet build "Navtrack.Core.csproj" -c Release -o /app

FROM build AS publish
RUN curl -sL https://deb.nodesource.com/setup_13.x | bash -
RUN apt-get install -y nodejs
RUN dotnet publish "Navtrack.Core.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Navtrack.Core.dll"]