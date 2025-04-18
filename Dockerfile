FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY BackEndChellengeAPI/BackEndChellengeAPI/BackEndChellengeAPI.csproj ./BackEndChellengeAPI/
RUN dotnet restore "./BackEndChellengeAPI/BackEndChellengeAPI.csproj"

COPY . .
WORKDIR /app/BackEndChellengeAPI
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "BackEndChellengeAPI.dll"]
