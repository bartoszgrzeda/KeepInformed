#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
#COPY ["src/Web/KeepInformed.Web.Api/KeepInformed.Web.Api.csproj", "src/Web/KeepInformed.Web.Api/"]
#COPY ["src/Infrastructure/KeepInformed.Infrastructure.MediatR/KeepInformed.Infrastructure.MediatR.csproj", "src/Infrastructure/KeepInformed.Infrastructure.MediatR/"]
#COPY ["src/Application/KeepInformed.Application.News/KeepInformed.Application.News.csproj", "src/Application/KeepInformed.Application.News/"]
#COPY ["src/Contracts/KeepInformed.Contracts.News/KeepInformed.Contracts.News.csproj", "src/Contracts/KeepInformed.Contracts.News/"]
#COPY ["src/Common/KeepInformed.Common/KeepInformed.Common.csproj", "src/Common/KeepInformed.Common/"]
#COPY ["src/Domain/KeepInformed.Domain.News/KeepInformed.Domain.News.csproj", "src/Domain/KeepInformed.Domain.News/"]
#COPY ["src/Common/KeepInformed.Common.Domain/KeepInformed.Common.Domain.csproj", "src/Common/KeepInformed.Common.Domain/"]
#COPY ["src/Infrastructure/KeepInformed.Infrastructure.Tvn/KeepInformed.Infrastructure.Tvn.csproj", "src/Infrastructure/KeepInformed.Infrastructure.Tvn/"]
#COPY ["src/Infrastructure/KeepInformed.Infrastructure.DbAccess/KeepInformed.Infrastructure.DbAccess.csproj", "src/Infrastructure/KeepInformed.Infrastructure.DbAccess/"]
#RUN dotnet restore "src/Web/KeepInformed.Web.Api/KeepInformed.Web.Api.csproj"
COPY src/ .
WORKDIR "/src/Web/KeepInformed.Web.Api"
RUN dotnet restore "KeepInformed.Web.Api.csproj"
RUN dotnet build "KeepInformed.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KeepInformed.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT development
ENTRYPOINT ["dotnet", "KeepInformed.Web.Api.dll"]