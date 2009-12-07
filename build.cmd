setlocal
set BUILD_TARGET=%~1
if "%1"=="" set /p BUILD_TARGET="Build Target: "
if not "%BUILD_TARGET%"=="" set BUILD_TARGET="/target:%BUILD_TARGET%"
msbuild build.xml %BUILD_TARGET% %2 %3 %4 %5 %6 %7 %8 %9
endlocal
pause