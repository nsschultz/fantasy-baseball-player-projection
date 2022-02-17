## Player Projection Service
* This service is the source of truth for the player projections.
  * Allows new projections to be uploaded as CSV files (one for batters and another for pitchers).
  * Extracts the contents of the CSV files and returns them to caller.

---
### Healthcheck:
* The service will fail a healthcheck if any of the CSV files are not accessible. 

---
### Dev Container
* Command for starting container:
```
docker run --rm -it --workdir /app -v $(pwd):/app nschultz/fantasy-baseball-common:1.0.3 bash
```
* Commands for installing VS Code extensions (from within container):
```
code --install-extension Fudge.auto-using
code --install-extension ms-dotnettools.csharp
```
* Dotnet Commands for Restore, Build, Test & Run
```
dotnet restore
dotnet build
dotnet test
dotnet run --project FantasyBaseball.PlayerProjectionService/FantasyBaseball.PlayerProjectionService.csproj
```
* View Swagger/Test Endpoints: http://localhost:5000/api/v1/projection/swagger/index.html