# Storied Take Home Assigment

## Steps to run

For this application, I created cmd script where we can execute both project (nextjs and asp.net core at the same time)

- Open the root folder name "Storied-TakeHome"
- Double click on run.cmd file
- Wait 8 seconds to start everything

## Run.cmd script

```bash
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
```