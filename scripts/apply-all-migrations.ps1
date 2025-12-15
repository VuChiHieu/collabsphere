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
Write-Host "Applying Migrations to All Databases" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

foreach ($service in $services) {
    Write-Host ""
    Write-Host "[$service] Updating database..." -ForegroundColor Yellow
    
    Push-Location "src/$service"
    dotnet ef database update --context "${service}DbContext"
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "OK $service database updated successfully" -ForegroundColor Green
    } else {
        Write-Host "ERROR $service database update failed" -ForegroundColor Red
    }
    
    Pop-Location
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Done!" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan