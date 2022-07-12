@echo off
:: Executes all tests that do not have the 'API' flag. 
::dotnet test --filter:"Category!=API" --logger:"trx;LogFileName=MyTest.trx"
:: Runs all tests marked with the 'Search' flag
::dotnet test --filter:"Category=Search" --logger:"trx;LogFileName=MyTest.trx"
:: Runs all tests marked with the 'Search' flag and with the flag 'german'
dotnet test --filter:"Category=Search&Category=german" --logger:"trx;LogFileName=MyTest.trx"
pause