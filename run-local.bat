@echo off
echo Iniciando API e aplicação MVC...

start cmd /k "cd GrupoColorado.WebAPI && dotnet run"
start cmd /k "cd GrupoColorado.WebAppMVC && dotnet run"

echo Tudo iniciado. Pressione qualquer tecla para sair.
pause > nul
