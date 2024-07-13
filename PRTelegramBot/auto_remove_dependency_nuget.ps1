param (
    [string]$nupkgPath
)

try {
    # Переименовываем .nupkg в .zip
    $zipPath = "$nupkgPath.zip"
    Rename-Item -Path "$nupkgPath" -NewName "$zipPath"

    # Извлекаем архив
    Expand-Archive -Path "$zipPath" -DestinationPath "TempExtract" -Force

    # Находим .nuspec файл
    $nuspecFile = Get-ChildItem -Path "TempExtract\*.nuspec" | Select-Object -First 1

    if ($nuspecFile -ne $null) {
        # Загружаем и выводим содержимое .nuspec
        [xml]$nuspecContent = Get-Content "$($nuspecFile.FullName)"
        
        # Выводим все зависимости для проверки
        $dependencies = $nuspecContent.package.metadata.dependencies.group.dependency
        foreach ($dep in $dependencies) {
            Write-Host "dependency: $($dep.id)"
        }

        # Находим зависимость по id
        $dependencyToRemove = $dependencies | Where-Object { $_.id -eq "Telegram.Bot" }

        if ($dependencyToRemove -ne $null) {
            # Удаляем зависимость
            $nuspecContent.package.metadata.dependencies.group.RemoveChild($dependencyToRemove)

            # Сохраняем изменения
            $nuspecContent.Save("$($nuspecFile.FullName)")

            Write-Host "Dependency 'Telegram.Bot' deleted from nuget package."
        } else {
            Write-Host "Dependency 'Telegram.Bot' not found."
        }
    }

    # Перепаковываем .nupkg
    Compress-Archive -Path "TempExtract\*" -DestinationPath "$zipPath" -Force

    # Переименовываем обратно .zip в .nupkg
    Rename-Item -Path "$zipPath" -NewName "$nupkgPath"

    # Удаляем временные файлы
    Remove-Item -Path "TempExtract" -Recurse -Force

} catch {
    Write-Host "Произошла ошибка: $_"
    exit 1
}
