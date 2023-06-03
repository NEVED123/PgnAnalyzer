@ECHO OFF

setlocal

set arg=%~1

if "%arg%"=="test" (
    dotnet test --filter FullyQualifiedName~MyTest
) else (
    dotnet run --project src/pgnanalyzer.csproj -- %*
)