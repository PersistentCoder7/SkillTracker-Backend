#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj", "SkillTracker.Profile.Api/"]
RUN dotnet restore "SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj"
COPY . .
WORKDIR "/src/SkillTracker.Profile.Api"
RUN dotnet build "SkillTracker.Profile.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkillTracker.Profile.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillTracker.Profile.Api.dll"]