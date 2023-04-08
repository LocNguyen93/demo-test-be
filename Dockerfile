#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base

RUN apt-get update
RUN apt-get install -y apt-utils
RUN apt-get install -y libgdiplus
RUN apt-get install -y libc6-dev
RUN apt-get install -y libx11-dev

RUN ln -s /lib/x86_64-linux-gnu/libdl-2.24.so /lib/x86_64-linux-gnu/libdl.so
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["RF.Web.Api/RF.Web.Api.csproj", "RF.Web.Api/"]
COPY ["Ext.Shared.Web/Ext.Shared.Web.csproj", "Ext.Shared.Web/"]
COPY ["Ext.Shared/Ext.Shared.csproj", "Ext.Shared/"]
COPY ["RF.Common/RF.Common.csproj", "RF.Common/"]
COPY ["RF.Web.Api.Services/RF.Web.Api.Services.csproj", "RF.Web.Api.Services/"]
COPY ["RF.Web.Api.DataAccess/RF.Web.Api.DataAccess.csproj", "RF.Web.Api.DataAccess/"]
COPY ["Ext.Shared.DataAccess.Dapper/Ext.Shared.DataAccess.Dapper.csproj", "Ext.Shared.DataAccess.Dapper/"]
COPY ["Ext.Shared.DataAccess/Ext.Shared.DataAccess.csproj", "Ext.Shared.DataAccess/"]
COPY ["RF.Web.Api.AppSettings/RF.Web.Api.AppSettings.csproj", "RF.Web.Api.AppSettings/"]
COPY ["RF.Web.Api.Entities/RF.Web.Api.Entities.csproj", "RF.Web.Api.Entities/"]
RUN dotnet restore "RF.Web.Api/RF.Web.Api.csproj"
COPY . .
WORKDIR "/src/RF.Web.Api"
RUN dotnet build "RF.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RF.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RF.Web.Api.dll"]
