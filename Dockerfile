FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /workspace

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=1 \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1 \
    NUGET_PACKAGES=/root/.nuget/packages \
    NUGET_FALLBACK_PACKAGES= \
    DOTNET_ROLL_FORWARD=LatestMajor

# Restore as a separate layer for better caching
COPY EDIFACT.sln ./
COPY NuGet.Config ./
COPY src/EDIFACT.csproj src/
COPY Test/Edifact_Test.csproj Test/
RUN dotnet restore EDIFACT.sln
RUN dotnet restore Test/Edifact_Test.csproj

# Copy the remaining source and run build + tests
COPY . .
RUN dotnet build EDIFACT.sln -c Release --no-restore
RUN dotnet build Test/Edifact_Test.csproj -c Release --no-restore
RUN dotnet test Test/Edifact_Test.csproj -c Release --no-build --no-restore

# Default container execution repeats the test suite
FROM build AS test-runner
ENTRYPOINT ["dotnet", "test", "Test/Edifact_Test.csproj", "-c", "Release", "--no-build", "--no-restore"]
