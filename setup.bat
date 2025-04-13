@echo off

:: Setup node
cd ./frontend/
call npm install
cd ..
echo Frontend setup complete.

:: Setup backend
cd ./backend/
call dotnet restore
cd ..
echo Backend setup complete.