Name "Simple.Net project"
OutFile "Simple.exe"
Icon "..\..\gfx\Icon.ico"
RequestExecutionLevel user
AutoCloseWindow true
BrandingText "contact@juanlopes.net"

;Default installation folder
InstallDir "$EXEDIR\example-project\"

Page directory
Page instfiles

Section
  SetOutPath $INSTDIR
  File /r /x _svn /x .svn /x bin /x build /x obj /x *.nsi /x *.suo /x *.cache /x *.log /x Simple.exe /x Simple.*.dll /x TestResult.xml *.* 
  File /oname=lib\Simple.Avalon.dll lib\Simple.Avalon.dll 
  ExecWait $OUTDIR\Simple.Gui.exe
  Delete $OUTDIR\Simple.Gui.exe
SectionEnd