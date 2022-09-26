#get base sdk image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

#Copy the csproj and restore dependencies
COPY . .
RUN dotnet restore RecklessSpeech.sln

#Copy the project files
RUN dotnet publish RecklessSpeech.Web/RecklessSpeech.Web.csproj

#faudra ajouter les projets de tests à publish mais pas les autres


#RUN dotnet publish -c Release -o out
#
##Generate runtime image
#FROM mcr.microsoft.com/dotnet/sdk:6.0
#WORKDIR /app
#EXPOSE 80
#COPY --from=build-env /app/out .
#
#ENTRYPOINT ["dotnet","RecklessSpeech.Web.dll"]