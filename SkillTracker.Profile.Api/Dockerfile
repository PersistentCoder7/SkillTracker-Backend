#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Cloud

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj", "SkillTracker.Profile.Api/"]
COPY ["SkillTracker.Common.Utils/SkillTracker.Common.Utils.csproj", "SkillTracker.Common.Utils/"]
COPY ["SkillTracker.Profile.Application/SkillTracker.Profile.Application.csproj", "SkillTracker.Profile.Application/"]
COPY ["SkillTracker.Profile.Domain/SkillTracker.Profile.Domain.csproj", "SkillTracker.Profile.Domain/"]
COPY ["SkillTracker.Profile.Data/SkillTracker.Profile.Data.csproj", "SkillTracker.Profile.Data/"]
COPY ["SkillTracker.Profile.Infrastructure/SkillTracker.Profile.Infrastructure.csproj", "SkillTracker.Profile.Infrastructure/"]
COPY ["SkillTracker.Common.MessageContracts/SkillTracker.Common.MessageContracts.csproj", "SkillTracker.Common.MessageContracts/"]
COPY ["SkillTracker.Search.Cache/SkillTracker.Search.Cache.csproj", "SkillTracker.Search.Cache/"]
COPY ["SkillTracker.Search.Domain/SkillTracker.Search.Domain.csproj", "SkillTracker.Search.Domain/"]
RUN dotnet restore "SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj"
COPY . .
WORKDIR "/src/SkillTracker.Profile.Api"
RUN dotnet build "SkillTracker.Profile.Api.csproj" -c Cloud -o /app/build

FROM build AS publish
RUN dotnet publish "SkillTracker.Profile.Api.csproj" -c Cloud -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillTracker.Profile.Api.dll"]