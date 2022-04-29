#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV SimpleProperty="Hello-from-BASE-dockerfile"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Edr.Api/Edr.Api.csproj", "Edr.Api/"]
RUN dotnet restore "Edr.Api/Edr.Api.csproj"
COPY . .
WORKDIR "/src/Edr.Api"
RUN dotnet build "Edr.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Edr.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV SimplePropety="hello-from-FINAL-dockerfile-doesnt-print-while-debugging"
ENTRYPOINT ["dotnet", "Edr.Api.dll"]