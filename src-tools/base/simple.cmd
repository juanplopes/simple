@echo off
if not exist "bin\server\Example.Project.Tools.exe" call build DryBuild "/p:Configuration=Debug"
util\Simple.Launcher bin\server Example.Project.Tools.exe
pause