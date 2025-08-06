#!/bin/bash

# Space.AI.NET() Game Launcher
echo "Building Space.AI.NET()..."
dotnet build

if [ $? -eq 0 ]; then
    echo "Build successful! Starting game..."
    echo "Note: This game requires an interactive console to run properly."
    echo "Use arrow keys to move, SPACE to shoot, S for screenshots, Q to quit."
    echo ""
    dotnet run
else
    echo "Build failed. Please check for errors."
fi
