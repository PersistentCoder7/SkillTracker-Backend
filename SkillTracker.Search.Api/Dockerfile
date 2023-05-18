#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Cloud

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SkillTracker.Search.Api/SkillTracker.Search.Api.csproj", "SkillTracker.Search.Api/"]
COPY ["SkillTracker.Common.Utils/SkillTracker.Common.Utils.csproj", "SkillTracker.Common.Utils/"]
COPY ["SkillTracker.Search.Application/SkillTracker.Search.Application.csproj", "SkillTracker.Search.Application/"]
COPY ["SkillTracker.Search.Cache/SkillTracker.Search.Cache.csproj", "SkillTracker.Search.Cache/"]
COPY ["SkillTracker.Search.Domain/SkillTracker.Search.Domain.csproj", "SkillTracker.Search.Domain/"]
RUN dotnet restore "SkillTracker.Search.Api/SkillTracker.Search.Api.csproj"
COPY . .
WORKDIR "/src/SkillTracker.Search.Api"
RUN dotnet build "SkillTracker.Search.Api.csproj" -c Cloud -o /app/build

FROM build AS publish
RUN dotnet publish "SkillTracker.Search.Api.csproj" -c Cloud -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillTracker.Search.Api.dll"]