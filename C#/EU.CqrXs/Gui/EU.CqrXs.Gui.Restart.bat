@echo off

timeout 2 > NUL
echo "Starting EU.CqrXs.Gui.exe %1 %2"
timeout 1 > NUL
start EU.CqrXs.Gui.exe "%1" "%2"
echo "Stared EU.CqrXs.Gui.exe %1 %2"
