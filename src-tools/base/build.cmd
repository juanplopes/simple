@echo off
setlocal
set BUILD_TARGET=%1
if %1*==* set /p BUILD_TARGET="Build Targets:"
if not %BUILD_TARGET%*==* set BUILD_TARGET="/target:%BUILD_TARGET%"
if not %BUILD_CONFIG%*==* set BUILD_CONFIG="/property:Configuration=%BUILD_CONFIG%"
msbuild build.xml %BUILD_TARGET% %BUILD_CONFIG%
endlocal