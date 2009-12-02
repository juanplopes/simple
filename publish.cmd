@echo off
setlocal
set /p GC_USER="Username: "
set /p GC_PASS="Password: "
msbuild build.xml /target:Publish "/property:GCUsername=%GC_USER%" "/property:GCPassword=%GC_PASS%"
endlocal
pause