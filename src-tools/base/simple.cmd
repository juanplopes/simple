@echo off
if not exist "bin\tools\Example.Project.Tools.exe" call build DryBuild "/p:Configuration=Debug"
util\Simple.Launcher bin\tools Example.Project.Tools.exe
pause