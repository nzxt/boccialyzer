cd ..
rmdir .\publish /S /Q

dotnet publish Boccialyzer.Web -r win-x64 -c Release -o ..\publish

pause
