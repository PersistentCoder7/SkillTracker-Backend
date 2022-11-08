FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

#Copy the solution files
COPY *.sln .
COPY SkillTracker.Domain.Core/SkillTracker.Domain.Core.csproj SkillTracker.Domain.Core/
COPY SkillTracker.Infrastructure.Bus/SkillTracker.Infrastructure.Bus.csproj SkillTracker.Infrastructure.Bus/
COPY SkillTracker.Infrastructure.IoC/SkillTracker.Infrastructure.IoC.csproj SkillTracker.Infrastructure.IoC/
COPY SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj SkillTracker.Profile.Api/
COPY SkillTracker.Profile.Application/SkillTracker.Profile.Application.csproj SkillTracker.Profile.Application/
COPY SkillTracker.Profile.Data/SkillTracker.Profile.Data.csproj SkillTracker.Profile.Data/
COPY SkillTracker.Profile.Domain/SkillTracker.Profile.Domain.csproj SkillTracker.Profile.Domain/

#Restore all the packages

RUN dotnet restore "SkillTracker.Domain.Core/SkillTracker.Domain.Core.csproj"
RUN dotnet restore "SkillTracker.Infrastructure.Bus/SkillTracker.Infrastructure.Bus.csproj"
RUN dotnet restore "SkillTracker.Infrastructure.IoC/SkillTracker.Infrastructure.IoC.csproj"
RUN dotnet restore "SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj"
RUN dotnet restore "SkillTracker.Profile.Application/SkillTracker.Profile.Application.csproj"
RUN dotnet restore "SkillTracker.Profile.Data/SkillTracker.Profile.Data.csproj"
RUN dotnet restore "SkillTracker.Profile.Domain/SkillTracker.Profile.Domain.csproj"

#Copy the other files under each project

COPY SkillTracker.Domain.Core/.				./SkillTracker.Domain.Core/
COPY SkillTracker.Infrastructure.Bus/.		./SkillTracker.Infrastructure.Bus/
COPY SkillTracker.Infrastructure.IoC/.		./SkillTracker.Infrastructure.IoC/
COPY SkillTracker.Profile.Api/.				./SkillTracker.Profile.Api/
COPY SkillTracker.Profile.Application/.		./SkillTracker.Profile.Application/
COPY SkillTracker.Profile.Data/.			./SkillTracker.Profile.Data/
COPY SkillTracker.Profile.Domain/.			./SkillTracker.Profile.Domain/

#WORKDIR /app/Experiment
#RUN dotnet publish -c Release -o out

FROM build AS publish
RUN dotnet publish "SkillTracker.Profile.Api/SkillTracker.Profile.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillTracker.Profile.Api.dll"]