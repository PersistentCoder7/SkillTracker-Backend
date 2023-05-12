FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

#Copy the solution files
COPY *.sln .
COPY SkillTracker.Common.Utils/SkillTracker.Common.Utils.csproj SkillTracker.Common.Utils/
COPY SkillTracker.Profile.Infrastructure/SkillTracker.Profile.Infrastructure.csproj SkillTracker.Profile.Infrastructure/
COPY SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj SkillTracker.Profile.Api/
COPY SkillTracker.Profile.Application/SkillTracker.Profile.Application.csproj SkillTracker.Profile.Application/
COPY SkillTracker.Profile.Data/SkillTracker.Profile.Data.csproj SkillTracker.Profile.Data/
COPY SkillTracker.Profile.Cache/SkillTracker.Profile.Cache.csproj SkillTracker.Profile.Data/
COPY SkillTracker.Profile.Domain/SkillTracker.Profile.Domain.csproj SkillTracker.Profile.Domain/

#Restore all the packages

RUN dotnet restore "SkillTracker.Common.Utils/SkillTracker.Common.Utils.csproj"
RUN dotnet restore "SkillTracker.Profile.Infrastructure/SkillTracker.Profile.Infrastructure.csproj"
RUN dotnet restore "SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj"
RUN dotnet restore "SkillTracker.Profile.Application/SkillTracker.Profile.Application.csproj"
RUN dotnet restore "SkillTracker.Profile.Data/SkillTracker.Profile.Data.csproj"
RUN dotnet restore "SkillTracker.Profile.Domain/SkillTracker.Profile.Domain.csproj"

#Copy the other files under each project

COPY SkillTracker.Common.Utils/.				./SkillTracker.Common.Utils/
COPY SkillTracker.Profile.Infrastructure/.		./SkillTracker.Profile.Infrastructure/
COPY SkillTracker.Profile.Api/.					./SkillTracker.Profile.Api/
COPY SkillTracker.Profile.Application/.			./SkillTracker.Profile.Application/
COPY SkillTracker.Profile.Data/.				./SkillTracker.Profile.Data/
COPY SkillTracker.Profile.Domain/.				./SkillTracker.Profile.Domain/

#WORKDIR /app/Experiment
#RUN dotnet publish -c Release -o out

FROM build AS publish
RUN dotnet publish "SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillTracker.Profile.Api.dll"]