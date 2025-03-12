$csprojPath = ".\virustotal-desktop.csproj"

function build {
    Write-Output "Building application..."
    dotnet restore $csprojPath
    dotnet build $csprojPath -c Release --no-restore
    dotnet publish $csprojPath -c Release --self-contained -r win-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true --no-build
}

function run {
    Write-Output "Starting built application..."
    dotnet run $csprojPath
}

function cleanup {
    Write-Output "Removing old binaries..."

    $folders = @(
        ".\bin\",
        ".\obj\"
    )

    foreach ($folder in $folders) {
        Remove-Item -LiteralPath $folder -Force -Recurse -ErrorAction SilentlyContinue
    }
}

if ($args.Count -eq 0) {
    Write-Output "No parameters provided. Usage:"
    Write-Output ".\build.ps1 [clean] [build] [run]"
} else {
    foreach ($parameter in $args) {
        switch ($parameter) {
            "build" {
                build
            }
            
            "clean" {
                cleanup
            }
            
            "run" {
                run
            }

            Default {
                Write-Host "Unkown paramter: $($parameter)"
            }
        }
    }
}
