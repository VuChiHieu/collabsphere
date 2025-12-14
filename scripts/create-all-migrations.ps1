$services = @(
    "UserService",
    "AcademicService",
    "ProjectService",
    "TeamService",
    "CollaborationService",
    "CommunicationService",
    "EvaluationService",
    "FileService",
    "NotificationService"
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Creating Migrations for All Services" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

foreach ($service in $services) {
    Write-Host ""
    Write-Host "[$service] Creating migration..." -ForegroundColor Yellow
    
    Push-Location "src/$service"
    dotnet ef migrations add InitialCreate --context "${service}DbContext" --output-dir Migrations
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "OK $service migration created successfully" -ForegroundColor Green
    } else {
        Write-Host "ERROR $service migration failed" -ForegroundColor Red
    }
    
    Pop-Location
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Done!" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan