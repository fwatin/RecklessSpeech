#get base sdk image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

#Copy the csproj and restore dependencies
COPY RecklessSpeech.AcceptanceTests/*.csproj .
COPY RecklessSpeech.Application.Core/*.csproj .
COPY RecklessSpeech.Application.Read/*.csproj .
COPY RecklessSpeech.Application.Write.Sequences/*.csproj .
COPY RecklessSpeech.Application.Write.Sequences.Tests/*.csproj .
COPY RecklessSpeech.Domain.Sequences/*.csproj .
COPY RecklessSpeech.Domain.Shared/*.csproj .
COPY RecklessSpeech.Infrastructure.Databases/*.csproj .
COPY RecklessSpeech.Infrastructure.Entities/*.csproj .
COPY RecklessSpeech.Infrastructure.Orchestration/*.csproj .
COPY RecklessSpeech.Infrastructure.Read/*.csproj .
COPY RecklessSpeech.Infrastructure.Read.Tests/*.csproj .
COPY RecklessSpeech.Infrastructure.Sequences/*.csproj .
COPY RecklessSpeech.Infrastructure.Sequences.Tests/*.csproj .
COPY RecklessSpeech.Shared.Tests/*.csproj .
COPY RecklessSpeech.Web/*.csproj .

#RUN dotnet restore RecklessSpeech.sln

#Copy the project files
#COPY . ./
#RUN dotnet publish ./RecklessSpeech.Web/RecklessSpeech.Web.csproj


#RUN dotnet publish -c Release -o out
#
##Generate runtime image
#FROM mcr.microsoft.com/dotnet/sdk:6.0
#WORKDIR /app
#EXPOSE 80
#COPY --from=build-env /app/out .
#
#ENTRYPOINT ["dotnet","RecklessSpeech.Web.dll"]