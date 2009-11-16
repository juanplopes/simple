@echo off
if not "%1"=="" set BUILD_TARGET="/target:%1"
if not "%2"=="" set BUILD_CONFIG="/property:Configuration=%2"
msbuild build.xml %BUILD_TARGET% %BUILD_CONFIG%