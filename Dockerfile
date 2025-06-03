FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
COPY . ./src
WORKDIR /src
RUN ls
RUN dotnet build "Line/Line.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Line/Line.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=public.ecr.aws/awsguru/aws-lambda-adapter:0.9.1 /lambda-adapter /opt/extensions/lambda-adapter
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Line.dll"]
