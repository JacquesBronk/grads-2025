@echo off
REM Running Tests
dotnet test tests/E2ERetroShop/E2ERetroShop.csproj --logger "xunit;LogFilePath=TestResults.xml"

REM Scoring Results
dotnet run --project tests/ScoreMe/ScoreMe.csproj