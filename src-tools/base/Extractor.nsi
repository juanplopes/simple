Name "Simple.Net project"
OutFile "Simple.exe"
Icon "..\..\gfx\Icon.ico"
RequestExecutionLevel user
AutoCloseWindow true
BrandingText "juanplopes@gmail.com"

;Default installation folder
InstallDir "$EXEDIR\sample-project\"

Page directory
Page instfiles

Section
  SetOutPath $INSTDIR
  File /r /x _svn /x .svn /x bin /x obj /x *.nsi /x *.suo /x *.cache /x *.log /x Simple.exe /x Simple.dll *.* 
  File /oname=lib\Simple.dll lib\Simple.dll 
  ExecWait $OUTDIR\Simple.Gui.exe
  Delete $OUTDIR\Simple.Gui.exe
SectionEnd