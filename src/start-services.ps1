# start-services.ps1
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "  CollabSphere Services Launcher" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host ""

# Danh sách services cần chạy
$services = @(
    @{Name="AuthService"; Port=5002; Color="Green"},
    @{Name="UserService"; Port=5001; Color="Yellow"},
    @{Name="ApiGateway"; Port=5000; Color="Magenta"}
    # Uncomment các services khác khi cần:
    # @{Name="AcademicService"; Port=5003; Color="Blue"},
    # @{Name="ProjectService"; Port=5004; Color="Cyan"},
    # @{Name="TeamService"; Port=5005; Color="Green"}
)

Write-Host "Starting services..." -ForegroundColor Green
Write-Host ""

foreach ($service in $services) {
    $servicePath = Join-Path $PSScriptRoot $service.Name
    
    if (Test-Path $servicePath) {
        Write-Host "[OK] Starting $($service.Name) on port $($service.Port)..." -ForegroundColor $service.Color
        
        $title = "CollabSphere - $($service.Name)"
        $color = $service.Color
        $command = "Write-Host '$title' -ForegroundColor $color; cd '$servicePath'; dotnet run --launch-profile http"
        
        Start-Process powershell -ArgumentList "-NoExit", "-Command", $command
        
        Start-Sleep -Seconds 1
    } else {
        Write-Host "[ERROR] Service not found: $servicePath" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "All services started!" -ForegroundColor Green
Write-Host "Press any key to exit..." -ForegroundColor Gray
Write-Host "=====================================" -ForegroundColor Cyan
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')