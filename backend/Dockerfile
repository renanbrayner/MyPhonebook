# Build  ----------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copia csproj e restaura para cache de camadas
COPY Phonebook.Domain/Phonebook.Domain.csproj Phonebook.Domain/
COPY Phonebook.Application/Phonebook.Application.csproj Phonebook.Application/
COPY Phonebook.Infrastructure/Phonebook.Infrastructure.csproj Phonebook.Infrastructure/
COPY Phonebook.API/Phonebook.API.csproj Phonebook.API/
RUN dotnet restore Phonebook.API/Phonebook.API.csproj

# copia todo o código e publica
COPY . .
WORKDIR /src/Phonebook.API
RUN dotnet publish -c Release -o /app/publish

# Runtime  --------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Phonebook.API.dll"]
