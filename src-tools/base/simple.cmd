@echo off
if not exist "bin\server\Example.Project.ModelServer.exe" call build DryBuild "/p:Configuration=Debug"
util\Simple.Launcher bin\server Example.Project.ModelServer.exe
pause