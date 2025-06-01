
@echo off
:: Caixa de entrada gráfica com interpretação de asteriscos como lista
setlocal EnableDelayedExpansion
for /f "delims=" %%i in ('powershell -command "Add-Type -AssemblyName Microsoft.VisualBasic; [Microsoft.VisualBasic.Interaction]::InputBox(\"Descreva a alteração (use * para listas)\", \"Changelog\", \"\")"') do set msg=%%i

:: Substituir asteriscos por - (formato Markdown visualmente mais amigável)
set msg=!msg:*=-!

git add .
git commit -m "!msg!"
git push

msg * "Commit enviado com sucesso!"
pause
