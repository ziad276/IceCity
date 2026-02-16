# IceCity

A .NET 8 console application for managing a heating system: define owners, houses, and heaters (electric or gas), then view usage and cost reports.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Build & run (local)

```bash
# From repo root
dotnet build IceCity.sln
dotnet run --project IceCity
```

## Run with Docker

From the repo root (context must include the solution and project):

```bash
docker build -t icecity -f IceCity/Dockerfile .
docker run -it icecity
```

## Project structure

- **IceCity/** – Main project
  - **Models/** – Owner, House, Heater (Electric/Gas), DailyUsage
  - **Services/** – CalculationService, Report

## License

Unlicensed; use as you like.
