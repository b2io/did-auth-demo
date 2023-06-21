#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DidAuthDemo.App/DidAuthDemo.App.csproj", "DidAuthDemo.App/"]
RUN dotnet restore "DidAuthDemo.App/DidAuthDemo.App.csproj"
COPY . .
WORKDIR "/src/DidAuthDemo.App"
RUN dotnet build "DidAuthDemo.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DidAuthDemo.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DidAuthDemo.App.dll"]