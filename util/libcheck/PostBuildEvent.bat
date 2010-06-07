@echo off
echo "Starting-Post Build Commands"
xcopy "C:\Documents and Settings\robvi\Desktop\temp\RefFiles"  "C:\Documents and Settings\robvi\Desktop\temp\bin\Release\RefFiles"  /I /E /Y
echo "Finished-Post Build Commands"

if errorlevel 1 goto CSharpReportError
goto CSharpEnd
:CSharpReportError
echo Project error: A tool returned an error code from the build event
exit 1
:CSharpEnd