@echo off
if not exist "bin\tools\Sample.Project.Tools.exe" call build DryBuild "/p:Configuration=Debug"
util\Simple.Launcher bin\tools Sample.Project.Tools.exe
pause