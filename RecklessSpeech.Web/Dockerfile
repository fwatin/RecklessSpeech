#get base sdk image
FROM mcr.microsoft.com/dotnet/core/sdk:net6 AS build-env
WORKDIR /app

#Copy the csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

#Copy the project files
COPY . ./
RUN dotnet publish -c Release -o out

#Generate runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet/net6
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet","RecklessSpeech.Web.dll"]