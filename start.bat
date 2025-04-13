@echo off

:: Start frontend server
echo Starting frontend server...
start "Frontend" cmd /c "cd ./frontend/ && npm run dev"

:: Start backend server
echo Starting backend server...
start "Backend" cmd /c "cd ./backend/ && dotnet run"

echo Servers are starting in separate windows.