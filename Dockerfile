# syntax=docker/dockerfile:experimental
FROM nschultz/fantasy-baseball-common:1.0.3 AS build
COPY . /app
ARG VERSION
ENV MAIN_PROJ=FantasyBaseball.PlayerProjectionService \
    SONAR_KEY=fantasy-baseball-player-projection
RUN --mount=type=cache,id=sonarqube,target=/root/.sonar/cache ./build.sh

FROM nschultz/base-csharp-runner:5.0.0
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "FantasyBaseball.PlayerProjectionService.dll"]