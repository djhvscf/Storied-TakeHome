@echo off
title App runner

cd Storied.TakeHome/Server
start dotnet run
cd ..
cd Client
start npm run dev

:: Open browser after 8 seconds
timeout /t 8 >nul
start http://localhost:3000/

echo Both servers are running!