@echo off

cd ..

timeout 5
start "Unsubscribe Handler" dotnet run unsubscribe

timeout 5
start "Subscribe Handler" dotnet run subscribe

