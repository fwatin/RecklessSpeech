#get base sdk image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

#Copy the csproj and restore dependencies
COPY . .
RUN dotnet restore RecklessSpeech.sln

#Copy the project files
RUN dotnet publish RecklessSpeech.Web/RecklessSpeech.Web.csproj -o /app
#faudra ajouter les projets de tests à publish mais pas les autres

## final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "RecklessSpeech.Web/RecklessSpeech.Web.dll"]