# Run tests with code coverage
dotnet test src/SolidEdgeCommunity.sln --collect:"XPlat Code Coverage" --results-directory ./coverage

# Generate human-readable report
reportgenerator "-reports:coverage\**\coverage.cobertura.xml" "-targetdir:coverage-report" "-reporttypes:Html;TextSummary"
