#!/bin/bash

# Start frontned server
(
  cd ./frontend/ &&
  npm run dev &
)

# Start backend server
(
  cd ./backend/ &&
  dotnet run &
)

wait