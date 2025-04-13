#!/bin/bash

# Setup node
cd ./frontend/
npm install
cd ../
echo "Frontend setup complete."

# Setup backend
cd ./backend/
dotnet restore
cd ../
echo "Backend setup complete."